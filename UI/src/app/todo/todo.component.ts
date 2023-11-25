import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Subject, takeUntil } from 'rxjs';

import { AlertingConfigService } from '../core/alerting/alerting-config.service';
import { AlertingService } from '../core/alerting/alerting.service';
import { CommonFunctionsService } from '../core/common/common-functions.service';
import { TodoApiClient, TodoItemCreateModel, TodoItemUpdateModel, TodoItemViewModel } from '../core/todo-services/todo-apiclient.service';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css']
})
export class TodoComponent implements OnInit, OnDestroy {

  addupdateTodoForm: FormGroup = this._formBuilder.group({});
  todoItemsViewModel: TodoItemViewModel[] = [];

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(private _formBuilder: FormBuilder, private _alertingService: AlertingService, private _commonFunctionsService: CommonFunctionsService, private _todoApiClient: TodoApiClient) {
  }

  /**
   * On init
   */
  ngOnInit(): void {
    this.initializeVariables();
    this.getTodos();
  }

  /**
   * On destroy
   */
  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Public methods
  // -----------------------------------------------------------------------------------------------------
  public onSubmitAddUpdateTodoForm(): void {
    if (this.addupdateTodoForm.valid) {
      let id: number = (this.addupdateTodoForm.get('id')?.value);
      if (id !== undefined && id !== null && id > 0) {
        this.editTodoItem();
      }
      else {
        this.addTodoItem();
      }
    }
  }

  public onClickCancelAddUpdateToDoForm(): void {
    this.addupdateTodoForm.reset();
  }

  public trackByTodoItems(index: number, todoItemViewModel: TodoItemViewModel) {
    return todoItemViewModel.id;
  }

  public onClickEditTodoItem(todoItemViewModel: TodoItemViewModel): void {
    this.addupdateTodoForm.reset();
    this.addupdateTodoForm.patchValue({
      id: todoItemViewModel.id,
      title: todoItemViewModel.title,
      isDone: todoItemViewModel.isDone,
    });
  }

  public onClickMarkDoneTodoItem(todoItemViewModel: TodoItemViewModel): void {
    let todoItemUpdateModel: TodoItemUpdateModel = TodoItemUpdateModel.fromJS(todoItemViewModel.toJSON());
    todoItemUpdateModel.isDone = !todoItemUpdateModel.isDone;
    this._todoApiClient.todoItemPUT(todoItemUpdateModel)
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe({
        next: () => {
        },
        error: (e) => {
          this.onErrorInMarkDoneTodoItem(e);
        },
      });
  }

  public onClickRemoveTodoItem(todoItemViewModel: TodoItemViewModel, index: number): void {
    this._todoApiClient.todoItemDELETE(todoItemViewModel.id as number)
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe({
        next: () => {
          this.todoItemsViewModel.splice(index, 1)
        },
        error: (e) => {
          this.onErrorInDeleteTodoItem(e);
        },
      });
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Private methods
  // -----------------------------------------------------------------------------------------------------
  private initializeVariables(): void {
    this.addupdateTodoForm = this._formBuilder.group({
      id: [null],
      title: [null, Validators.required],
      isDone: [false]
    });
    this.todoItemsViewModel = [];

  }

  private getTodos(): void {
    this._todoApiClient.todoItemAll()
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe({
        next: (apiResult: TodoItemViewModel[]) => {
          this.todoItemsViewModel = apiResult;
        },
        error: (e) => {
          this.onErrorInGetTodos(e);
        },
      });
  }

  private onErrorInGetTodos(e: any): void {
    console.log(e);
    this._alertingService.error(AlertingConfigService.getAlertingConfigForErrorInGetTodos());
  }

  private addTodoItem(): void {
    let todoItemCreateModel: TodoItemCreateModel = new TodoItemCreateModel();
    todoItemCreateModel.title = this.addupdateTodoForm.get('title')?.value;

    this._todoApiClient.todoItemPOST(todoItemCreateModel)
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe({
        next: (apiResult: number) => {
          let todoItemViewModel: TodoItemViewModel = new TodoItemCreateModel();
          todoItemViewModel.id = apiResult;
          todoItemViewModel.title = todoItemCreateModel.title;
          todoItemViewModel.isDone = false;
          this.todoItemsViewModel.push(todoItemViewModel);
        },
        error: (e) => {
          this.onErrorInAddTodoItem(e);
        },
      });
  }

  private onErrorInAddTodoItem(e: any): void {
    console.log(e);
    this._alertingService.error(AlertingConfigService.getAlertingConfigForErrorInAddTodoItem());
  }

  private editTodoItem(): void {
    let todoItemUpdateModel: TodoItemUpdateModel = new TodoItemUpdateModel();
    todoItemUpdateModel.id = this.addupdateTodoForm.get('id')?.value;
    todoItemUpdateModel.title = this.addupdateTodoForm.get('title')?.value;
    todoItemUpdateModel.isDone = this.addupdateTodoForm.get('isDone')?.value;

    this._todoApiClient.todoItemPUT(todoItemUpdateModel)
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe({
        next: () => {
        },
        error: (e) => {
          this.onErrorInEditTodoItem(e);
        },
      });
  }

  private onErrorInEditTodoItem(e: any): void {
    console.log(e);
    this._alertingService.error(AlertingConfigService.getAlertingConfigForErrorInEditTodoItem());
  }

  private onErrorInMarkDoneTodoItem(e: any): void {
    console.log(e);
    this._alertingService.error(AlertingConfigService.getAlertingConfigForErrorInMarkDoneTodoItem());
  }

  private onErrorInDeleteTodoItem(e: any): void {
    console.log(e);
    this._alertingService.error(AlertingConfigService.getAlertingConfigForErrorInDeleteTodoItem());
  }
}

