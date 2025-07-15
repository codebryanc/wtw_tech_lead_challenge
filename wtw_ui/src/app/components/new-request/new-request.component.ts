import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { RequestService } from '../../service/remote/request.service';
import { RequestTypeService } from '../../service/remote/request-type.service';
import { RequestStatusService } from '../../service/remote/request-status.service';
import { RequestStatusEntity } from '../../models/requestStatusEntity';
import { RequestTypeEntity } from '../../models/requestTypeEntity';
import { RequestCreateDto } from '../../models/dtos/request.dto';
import { MessageService } from '../../service/common/message.service';

interface DynamicProperty {
  key: string;
  value: any;
  type: 'text' | 'number' | 'decimal' | 'date';
  isValid: boolean;
}

@Component({
  selector: 'app-new-request',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './new-request.component.html',
  styleUrl: './new-request.component.scss'
})
export class NewRequestComponent implements OnInit {
  
  @Output() requestCreated = new EventEmitter<void>();
  
  // Form fields
  selectedTypeId: string = '';
  selectedStatusId: string = '';
  
  // Dynamic properties (max 3)
  properties: DynamicProperty[] = [];
  maxProperties = 3;
  
  // Available options
  availableStatuses: RequestStatusEntity[] = [];
  availableTypes: RequestTypeEntity[] = [];
  
  // Property types
  propertyTypes = [
    { value: 'text', label: 'Text' },
    { value: 'number', label: 'Number' },
    { value: 'decimal', label: 'Decimal' },
    { value: 'date', label: 'Date' }
  ];
  
  // Form state
  isSubmitting = false;
  isFormValid = false;
  
  constructor(
    private _requestService: RequestService,
    private _requestTypeService: RequestTypeService,
    private _requestStatusService: RequestStatusService,
    private _messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.loadOptions();
  }

  private loadOptions(): void {
    this.availableStatuses = this._requestStatusService.getRequestStatus();
    this.availableTypes = this._requestTypeService.getRequestTypes();
  }

  // Property management
  addProperty(): void {
    if (this.properties.length < this.maxProperties) {
      this.properties.push({
        key: '',
        value: '',
        type: 'text',
        isValid: false
      });
      this.validateForm();
    }
  }

  removeProperty(index: number): void {
    this.properties.splice(index, 1);
    this.validateForm();
  }

  onPropertyChange(index: number): void {
    const property = this.properties[index];
    this.validateProperty(property);
    this.validateForm();
  }

  onPropertyTypeChange(index: number): void {
    const property = this.properties[index];
    // Reset value when type changes
    property.value = '';
    this.validateProperty(property);
    this.validateForm();
  }

  private validateProperty(property: DynamicProperty): void {
    if (!property.key.trim()) {
      property.isValid = false;
      return;
    }

    switch (property.type) {
      case 'text':
        property.isValid = property.value && property.value.trim().length > 0;
        break;
      case 'number':
        property.isValid = property.value !== '' && !isNaN(Number(property.value)) && Number.isInteger(Number(property.value));
        break;
      case 'decimal':
        property.isValid = property.value !== '' && !isNaN(Number(property.value));
        break;
      case 'date':
        property.isValid = property.value && !isNaN(new Date(property.value).getTime());
        break;
      default:
        property.isValid = false;
    }
  }

  private validateForm(): void {
    const hasValidType = this.selectedTypeId.trim() !== '';
    const hasValidStatus = this.selectedStatusId.trim() !== '';
    const allPropertiesValid = this.properties.length === 0 || this.properties.every(p => p.isValid);
    
    this.isFormValid = hasValidType && hasValidStatus && allPropertiesValid;
  }

  onSelectionChange(): void {
    this.validateForm();
  }

  // Form submission
  onSubmit(): void {
    if (!this.isFormValid || this.isSubmitting) {
      return;
    }

    this.isSubmitting = true;

    // Build data object from properties
    const dataObject: any = {};
    this.properties.forEach(property => {
      let value = property.value;
      
      // Convert values based on type
      switch (property.type) {
        case 'number':
          value = parseInt(value, 10);
          break;
        case 'decimal':
          value = parseFloat(value);
          break;
        case 'date':
          value = new Date(value).toISOString();
          break;
        // text remains as string
      }
      
      dataObject[property.key] = value;
    });

    const createDto: RequestCreateDto = {
      rtyId: this.selectedTypeId,
      resId: this.selectedStatusId,
      data: Object.keys(dataObject).length > 0 ? JSON.stringify(dataObject) : undefined
    };

    this._requestService.createRequest(createDto).subscribe({
      next: (response) => {
        if(response) {
          this._messageService.showMessage("Request created successfully");
        }

        this.resetForm();
        this.isSubmitting = false;
        this.requestCreated.emit();
      },
      error: (error) => {
        this.isSubmitting = false;

        if(error) {
          this._messageService.showError('Ups!!! Cannot create request')
        }
      }
    });
  }

  resetForm(): void {
    this.selectedTypeId = '';
    this.selectedStatusId = '';
    this.properties = [];
    this.isFormValid = false;
  }

  // Helper methods for template
  getPropertyInputType(type: string): string {
    switch (type) {
      case 'number':
      case 'decimal':
        return 'number';
      case 'date':
        return 'date';
      default:
        return 'text';
    }
  }

  getPropertyInputStep(type: string): string | null {
    switch (type) {
      case 'decimal':
        return '0.01';
      default:
        return null;
    }
  }
}