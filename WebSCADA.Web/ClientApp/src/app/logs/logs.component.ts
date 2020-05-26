import { Component, OnInit } from '@angular/core';
import { LogService } from './shared/logs.service';

@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html'
})
export class LogsComponent implements OnInit {

  logs: Log[];

  constructor(private schemaService: LogService) { }

  ngOnInit(): void {
    this.getLogs();
  }

  getLogs(): void {
    this.schemaService.getLogs().subscribe(logs => {
      this.logs = logs;
    });
  }

}
