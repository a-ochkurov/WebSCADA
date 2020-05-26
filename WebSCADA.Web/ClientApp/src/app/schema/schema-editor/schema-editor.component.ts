import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { fabric } from 'fabric';
import { SchemaService } from '../shared/schema.service';

@Component({
  selector: 'app-schema-editor',
  templateUrl: './schema-editor.component.html',
  styleUrls: ['./schema-editor.component.scss']
})
export class SchemaEditorComponent implements OnInit {

  textForm = new FormGroup({
    address: new FormControl(''),
    type: new FormControl(''),
    text: new FormControl(''),
  });
  canvas: any;
  schema: Schema;
  api = 0;
  textShow = false;
  constructor(private schemaService: SchemaService) { }

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
    this.schema = {
      name: 'test',
      data: '',
    };
    console.log(this.schema);
    this.schemaService.getShema('test').subscribe(schema => {
      if ('data' in schema && (schema !== null || schema !== undefined)) {

        this.schema = schema;
        if (schema.data != null) {
          this.canvas.loadFromJSON(schema.data, this.canvas.renderAll.bind(this.canvas), function(o, object) {
            fabric.log(o, object);
        });
        }
      }
    });
  }

  addCircle() {
    const circle = new fabric.Circle({
      radius: 30,
      fill: '',
      top: 100,
      left: 100,
      stroke: 'black',
      strokeWidth: 1 });

    this.canvas.add(circle);
  }

  addRectangle() {
    const rect = new fabric.Rect({
      fill: '',
      top: 100,
      left: 100,
      width: 150,
      height: 100,
      stroke: 'black',
      strokeWidth: 1
    });

    this.canvas.add(rect);
  }

  addLine() {
    const line = new fabric.Line([100, 0, 0, 0], {
      left: 100,
      top: 100,
      stroke: 'black'
    });

    this.canvas.add(line);
  }

  addText() {
    const text = new fabric.Textbox(this.textForm.value.text, {
      address: this.textForm.value.address,
      typePLC: this.textForm.value.type
    });

    this.canvas.add(text);

    this.textShow = false;
  }

  saveShema() {
    console.log(this.schema);
    const canvasData = JSON.stringify(this.canvas);
    this.schema.data = canvasData;

    this.schemaService.saveShema(this.schema).subscribe();
    console.log(canvasData);
  }

  deleteElem() {
    this.canvas.remove(this.canvas.getActiveObject());
  }
}
