import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';

import { MessageEntity } from '../../../models/messageEntity';
import { ToolService } from '../../../service/common/tool.service';
import { ButtonEntity } from '../../../models/buttonEntity';
import { MessageService } from '../../../service/common/message.service';

@Component({
  selector: 'app-message',
  standalone: true,
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss'],
  imports: [
    // Base
    CommonModule,
    // Modal
    MatDialogModule,
    // UX
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
  ],
})
export class MessageComponent {

  // design data
  private closeDialogClass = 'mat-dialog-close';

  constructor(@Inject(MAT_DIALOG_DATA) public data: MessageEntity,
    private _toolsService: ToolService,
    private _matDialog: MatDialog,
    private _messageService: MessageService) {

  }

  // Methods
  launchAction(action: ButtonEntity) {
    if(this._toolsService.isExists(action)){
      if(this._toolsService.isExists(action.class) && action.class.includes(this.closeDialogClass)) {
        this._matDialog.closeAll();
      }

      // Action result
      if(this._toolsService.isExists(action.actionResult)) {
        action.actionResult.subscribe((data)=> {
          if(this._toolsService.isExists(data)) {
            
            // Current Modals
            this._matDialog.closeAll();
            
            if(data) {
              // This for show correctly messages
              if(data.result) {
                this._messageService.showSuccess(action.messageOk);
                
                this.launchPostAction(action);
              }
              else {
                this._messageService.showError(action.messageError);
              }

              // In other way there are database messages?
              if(data.databaseMessage) {
                // Inform user
                this._messageService.showAlert(data.databaseMessage);
              }
            }
            else {
              this._messageService.showError("Imposible recibir respuesta del servidor");
            }
          }
          else {

            // Current Modals
            this._matDialog.closeAll();

            this._messageService.showError(action.messageError);
            
            this._toolsService.instrumentation(action.consoleLogFail);
          }
        });
      }

      // Action result list
      if(this._toolsService.isExists(action.actionResultList)) {
        action.actionResultList.subscribe((dataList)=> {
          if(this._toolsService.isExists(dataList)) {
            
            // Current Modals
            this._matDialog.closeAll();
            
            // This for show correctly messages
            if(dataList) {
              let data = dataList[0];
              if(data.result) {
                this._messageService.showSuccess(action.messageOk);
                
                this.launchPostAction(action);
              }
              else {
                this._messageService.showError(action.messageError);
              }
            }
            else {
              this._messageService.showError("Imposible recibir respuesta del servidor");
            }
          }
          else {
            this._toolsService.instrumentation(action.consoleLogFail);
          }
        });
      }
      else {
        this.launchPostAction(action);
      }
    }
  }

  launchPostAction(action: ButtonEntity) : void {
    // Only for modify data after subscribe ends
    if(this._toolsService.isExists(action)) {
      action.postAction();
    }
  }
}