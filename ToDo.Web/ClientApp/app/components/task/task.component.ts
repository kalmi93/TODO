import { Component, OnInit } from '@angular/core';
import { TaskListService } from '../../services/tasklist.service';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
    templateUrl: './task.component.html',
})
export class ViewTaskComponent implements OnInit {

    taskId: number = 0;
    task: any;
    users: any[];
    errMessage: string = "";
    editable: any = true;

    constructor(private taskListService: TaskListService,
                private route: ActivatedRoute,
                private router: Router) {

        route.params.subscribe(p => {
            this.taskId = +p['id'];
            if (isNaN(this.taskId) || this.taskId <= 0) {
                router.navigate(['/tasklist']);
                return;
            }
        });
    }

    ngOnInit() {
        this.taskListService.getTask(this.taskId).subscribe(
            task => {
                this.task = task;
                if (this.task.taskStatus == 2)
                    this.editable = false;
                this.taskListService.getUsers().subscribe(
                    users => this.users = users,
                    err => {
                        this.router.navigate(['/tasklist']);
                    }
                );
            },
            err => {
                if (err.status == 404)
                {
                    this.router.navigate(['/tasklist']);
                }
            });
        
    }

    submit() {
        delete this.task.user;
        //console.log(this.task);
        //var result = this.taskListService.update(this.task);
        //console.log(result.subscribe(p => console.log(p), err => console.log(err)));
        //this.router.navigate(['/tasklist']);

        this.taskListService.update(this.task).subscribe(
            response => {
                this.router.navigate(['/tasklist']);
            },
            err => {
                this.errMessage = err.text();
            });
        
    }
}