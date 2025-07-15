import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { LoadingService } from '../../service/common/loading.service';
import { MessageService } from '../../service/common/message.service';
import { ToolService } from '../../service/common/tool.service';
import { LoadingComponent } from '../shared/loading/loading.component';
import { MessageComponent } from '../shared/message/message.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    // Base
    RouterOutlet
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
    // [Constructor]
    constructor(
      private _loadingService: LoadingService,
      private _messageService: MessageService,
      private _toolService: ToolService,
      private _matDialog: MatDialog
    ) {
    }

    // Angular NG
    ngOnInit() {
      // Necessary for Modals
      this.LoadingServiceSubscribe();
      this.MessageServiceSubscribe();
    }

    ngOnDestroy() {
      // Necessary for Modals
      this._loadingService.changeLoading.unsubscribe();
      this._messageService.changeMessage.unsubscribe();
    }

    // subscribe
    LoadingServiceSubscribe() {
      // Loading service
      this._loadingService.changeLoading.subscribe(modelLoading => {
        if(this._toolService.isExists(modelLoading) && modelLoading.loading) {
          // Model loading
          let dialogData = this._loadingService.dialogConfig;
          dialogData.id = modelLoading.id;
          // Dialog open
          this._matDialog.open(LoadingComponent, dialogData);
        }
        else{
          let dialogOpen = this._matDialog.getDialogById(modelLoading.id);
          if(this._toolService.isExists(dialogOpen)) {
            dialogOpen.close();
          }
        }
      })
    }

    MessageServiceSubscribe() {
      // Message Service
      this._messageService.changeMessage.subscribe(data => {
        // config dialog
        let config = this._messageService.dialogConfig;
        config.data = data;
        // open Message dialog
        this._matDialog.open(MessageComponent, config);
      });
    }
}
