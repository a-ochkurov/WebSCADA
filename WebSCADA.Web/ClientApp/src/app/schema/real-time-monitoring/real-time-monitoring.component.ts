import { Component, OnInit } from '@angular/core';
import { Subscription, timer, Observable, Subject } from 'rxjs';
import { switchMap, takeUntil, catchError } from 'rxjs/operators';
import { fabric } from 'fabric';
import { SchemaService } from '../shared/schema.service';
import { PlcDataService } from '../shared/plcData.service';


@Component({
  selector: 'app-real-time-monitoring',
  templateUrl: './real-time-monitoring.component.html'
})
export class RealTimeMonitoringComponent implements OnInit {
  canvas: any;
  schema: Schema;
  timer: any;

  subscription: Subscription;
  listPLCData: PLCData[] = [];

  constructor(private schemaService: SchemaService,
              private plcDataService: PlcDataService) { }

  ngOnInit(): void {
    fabric.Object.prototype.toObject = (function(toObject: any) {
      return function(properties) {
          return fabric.util.object.extend(toObject.call(this, properties), {
            address: this.address
          });
      };
    })(fabric.Object.prototype.toObject);
    fabric.Object.prototype.toObject = (function(toObject: any) {
      return function(properties) {
          return fabric.util.object.extend(toObject.call(this, properties), {
            typePLC: this.typePLC
          });
      };
    })(fabric.Object.prototype.toObject);

    this.canvas = new fabric.Canvas('myCanvas');
    this.canvas.selection = false;

    this.schemaService.getShema('test').subscribe(schema => {
      this.schema = schema;
      if (schema.data != null) {
        this.canvas.loadFromJSON(schema.data, this.canvas.renderAll.bind(this.canvas), function(o, object) {
      });
      }

      this.canvas.getObjects().forEach(element => {
        if (element.address != null)
        {
          const temp: PLCData =
          {
            address: +element.address,
            data: '',
            typePLC: +element.typePLC
          };

          this.listPLCData.push(temp);
        }
      });

      this.timer = setInterval(() => {
      this.plcDataService.getdata(this.listPLCData).subscribe(result => {
        this.listPLCData = result;
        console.log(this.listPLCData);
        this.listPLCData.forEach(element => {
          this.canvas.getObjects().forEach(el => {
            if (el.address == element.address && el.typePLC == element.typePLC) {
              console.log(el);
              el.set('text', element.data);
              this.canvas.renderAll();
            }
        });
        });
      });
      }, (5000));

      this.canvas.on('text:changed', e => {
        console.log('text:changed');
        const temp: PLCData = {
          address: +e.target.address,
          data: e.target.text,
          typePLC: +e.target.typePLC
        };
        this.plcDataService.setdata(temp).subscribe();
      });
    });
  }
}
