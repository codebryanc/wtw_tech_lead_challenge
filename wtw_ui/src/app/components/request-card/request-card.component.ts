import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RequestsEntity } from '../../models/requestsEntity';
import { ToolService } from '../../service/common/tool.service';

interface DataField {
  key: string;
  value: any;
  type: string;
  formattedValue: string;
}

@Component({
  selector: 'app-request-card',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './request-card.component.html',
  styleUrl: './request-card.component.scss'
})
export class RequestCardComponent {
  @Input() request!: RequestsEntity;

  get parsedData(): DataField[] {
    if (!this.request.data) return [];
    
    try {
      const jsonData = JSON.parse(this.request.data);
      return this.parseObjectToFields(jsonData);
    } catch (error) {
      return [{ 
        key: 'data', 
        value: this.request.data, 
        type: 'string', 
        formattedValue: this.request.data 
      }];
    }
  }

  constructor(private _toolService: ToolService) {

  }

  getStatusClass(status: string): string {
    if (!status) return '';
    
    const statusLower = status.toLowerCase();
    
    if (statusLower === 'approved') return 'status-approved';
    if (statusLower === 'pending') return 'status-pending';
    if (statusLower === 'rejected') return 'status-rejected';
    
    return '';
  }

  private parseObjectToFields(obj: any): DataField[] {
    const fields: DataField[] = [];
    
    for (const [key, value] of Object.entries(obj)) {
      const type = this.getDataType(value);
      const formattedValue = this.formatValue(value, type);
      
      fields.push({
        key,
        value,
        type,
        formattedValue
      });
    }
    
    return fields;
  }

  private getDataType(value: any): string {
    if (typeof value === 'number') return 'number';
    if (typeof value === 'string') {
      if (this._toolService.isValidDate(value)) return 'date';
      return 'string';
    }
    return 'string';
  }

  private formatValue(value: any, type: string): string {
    switch (type) {
      case 'number':
        return value.toLocaleString();
      case 'date':
        return new Date(value).toLocaleDateString();
      case 'string':
      default:
        return String(value);
    }
  }

}