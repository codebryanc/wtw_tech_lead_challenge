import { EventEmitter, Injectable, Output } from '@angular/core';

import { MatDialogConfig } from '@angular/material/dialog';

import { MessageEntity } from '../../models/messageEntity';
import { WindowService } from './window.service';
import { ButtonEntity } from '../../models/buttonEntity';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  public dialogConfig = new MatDialogConfig();
  
  @Output() changeMessage: EventEmitter<MessageEntity> = new EventEmitter();

  constructor(private _windowService: WindowService) {
    this.dialogConfig.autoFocus = true;
    this.dialogConfig.width = this.getWidthModal();
  }

  // Functions  
  getWidthModal() {
    
    if(((this._windowService.innerWidth * 100) / 100) < 900) {
      return "85%";
    }
    else {
      return "55%";
    }
  }

  getMessageData(message: string) {
    let data = new MessageEntity();
    data.message = message;
    data.type = 'Información';
    data.class = 'info';
    data.classButton = 'infoButton';
    return data;
  }

  getAlertData(message: string) {
    let data = new MessageEntity();
    data.message = message;
    data.type = 'Alerta';
    data.class = 'alert';
    data.classButton = 'alertButton';
    return data;
  }

  getErrorData(message: string) {
    let data = new MessageEntity();
    data.message = message;
    data.type = 'Error';
    data.class = 'error';
    data.classButton = 'errorButton';
    return data;
  }

  getSuccessData(message: string) {
    let data = new MessageEntity();
    data.message = message;
    data.type = 'Confirmación';
    data.class = 'success';
    data.classButton = 'successButton';
    return data;
  }

  // Methods
  showMessage(message: string) {
    // Message data
    let data = this.getMessageData(message);
    // Emit message
    this.changeMessage.emit(data);
  }

  showAlert(message: string) {
    // Message data
    let data = this.getAlertData(message);
    // Emit message
    this.changeMessage.emit(data);
  }

  showError(message: string) {
    // Message data
    let data = this.getErrorData(message);
    // Emit message
    this.changeMessage.emit(data);
  }

  showSuccess(message: string) {
    // Message data
    let data = this.getSuccessData(message);
    // Emit message
    this.changeMessage.emit(data);
  }
  
  // With actions
  showMessageActions(message: string, actions: ButtonEntity[]) {
    // Message data
    let data = this.getMessageData(message);
    data.actions = actions;
    // Emit message
    this.changeMessage.emit(data);
  }

  showAlertActions(message: string, actions: ButtonEntity[]) {
    // Message data
    let data = this.getAlertData(message);
    data.actions = actions;
    // Emit message
    this.changeMessage.emit(data);
  }
  
  showErrorActions(message: string, actions: ButtonEntity[]) {
    // Message data
    let data = this.getErrorData(message);
    data.actions = actions;
    // Emit message
    this.changeMessage.emit(data);
  }

  showSuccessActions(message: string, actions: ButtonEntity[]) {
    // Message data
    let data = this.getSuccessData(message);
    data.actions = actions;
    // Emit message
    this.changeMessage.emit(data);    
  }
}
