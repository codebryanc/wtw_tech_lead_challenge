import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RequestsEntity } from '../../models/requestsEntity';

@Component({
  selector: 'app-request-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './request-card.component.html',
  styleUrl: './request-card.component.scss'
})
export class RequestCardComponent {
  @Input() request!: RequestsEntity;
}