import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class TaskListService {
    private readonly taskListEndpoint = '/api/tasks';
    constructor(private http: Http) { }


    create(task: any) {
        return this.http.post(this.taskListEndpoint, task)
            .map(res => res.json());
    }

    getTask(id: any) {
        return this.http.get(this.taskListEndpoint + '/' + id)
            .map(res => res.json());
    }

    getTaskList() {
        return this.http.get(this.taskListEndpoint)
            .map(res => res.json());
    }

    delete(id: any) {
        return this.http.delete(this.taskListEndpoint + '/' + id)
            .map(res => res.json());
    }

    update(task: any) {
        return this.http.put(this.taskListEndpoint, task)
            .map(res => res.json());
    }

    getUsers() {
        return this.http.get('/api/getusers')
            .map(res => res.json());
    }
}