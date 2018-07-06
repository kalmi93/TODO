import { Component, OnInit } from '@angular/core';
import { TaskListService } from '../../services/tasklist.service'; 

@Component({
    selector: 'tasklist',
    templateUrl: './tasklist.component.html',
    styleUrls: ['./tasklist.component.css']
})
export class TaskListComponent implements OnInit {

    taskList: any;

    constructor(private taskListService: TaskListService) { }

    ngOnInit() {
        this.taskListService.getTaskList().subscribe(list => this.taskList = list);
    }

    getStatus(status: number) {
        switch (status) {
            case (1):
                return "Open";
            case (2):
                return "Closed";
            default:
                return "Not Set";
        }
    }
}
