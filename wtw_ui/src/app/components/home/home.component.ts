import { Component } from '@angular/core';

import { HeaderComponent } from '../header/header.component';
import { AllRequestComponent } from '../all-request/all-request.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [
    CommonModule,
    // Component
    HeaderComponent,
    AllRequestComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
