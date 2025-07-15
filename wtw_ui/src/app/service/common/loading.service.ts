import { Injectable, Output, EventEmitter } from '@angular/core';

import { MatDialogConfig } from '@angular/material/dialog';

import { loadingEntity } from '../../models/loadingEntity';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  public isLoading = false;
  public dialogConfig = new MatDialogConfig();

  @Output() changeLoading: EventEmitter<loadingEntity> = new EventEmitter();
  
  constructor() {
    this.dialogConfig.disableClose = true;
    this.dialogConfig.autoFocus = true;
    this.dialogConfig.width = "40%";
  }
  
  // Toggle loading service
  toggle(event: boolean, id: string) {
  
    // Model
    let modelLoading = new loadingEntity();
    modelLoading.id = id;
    modelLoading.loading = event;
    
    // emit
    this.isLoading = event;
    this.changeLoading.emit(modelLoading);
  }
}
