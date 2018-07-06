import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { TaskListComponent } from './components/tasklist/tasklist.component';
import { ViewTaskComponent } from './components/task/task.component';

import { TaskListService } from './services/tasklist.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        TaskListComponent,
        ViewTaskComponent,

    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'tasklist', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },           
            { path: 'tasklist', component: TaskListComponent },
            { path: 'viewtask/:id', component: ViewTaskComponent },
            { path: '**', redirectTo: 'home' },
        ])
    ],
    providers: [
        TaskListService,
    ]
})
export class AppModuleShared {
}
