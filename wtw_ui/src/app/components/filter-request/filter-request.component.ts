import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { RequestTypeService } from '../../service/remote/request-type.service';
import { RequestStatusService } from '../../service/remote/request-status.service';
import { RequestStatusEntity } from '../../models/requestStatusEntity';
import { RequestTypeEntity } from '../../models/requestTypeEntity';
import { RequestFilterDto } from '../../models/dtos/request.dto';

@Component({
  selector: 'app-filter-request',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './filter-request.component.html',
  styleUrl: './filter-request.component.scss'
})
export class FilterRequestComponent implements OnInit {
  
  @Output() filtersChanged = new EventEmitter<RequestFilterDto>();
  @Output() filtersCleared = new EventEmitter<void>();

  // Filter properties
  statusFilter: string = '';
  typeFilter: string = '';
  searchText: string = '';

  // Common
  minSearchLength = 3;
  
  // Available options for filters
  availableStatuses: RequestStatusEntity[] = [];
  availableTypes: RequestTypeEntity[] = [];

  constructor(
    private _requestTypeService: RequestTypeService,
    private _requestStatusService: RequestStatusService
  ) {}

  ngOnInit(): void {
    this.getFilterOptions();
  }

  private getFilterOptions(): void {
    this.availableStatuses = this._requestStatusService.getRequestStatus();
    this.availableTypes = this._requestTypeService.getRequestTypes();
  }

  onFilterChange(): void {
    // Check if searchText has minimum 3 characters when present
    const hasValidSearch = !this.searchText || this.searchText.length >= this.minSearchLength;
    
    // If no filters are applied or search text is invalid, emit empty filter
    if ((!this.statusFilter && !this.typeFilter && !this.searchText) || !hasValidSearch) {
      this.filtersChanged.emit({});
      return;
    }

    // Build filter object for backend
    const filter: RequestFilterDto = {};

    // Map selected values to IDs
    if (this.statusFilter) {
      const selectedStatus = this.availableStatuses.find(s => s.resId === this.statusFilter);
      if (selectedStatus) {
        filter.RequestStatusId = selectedStatus.resId;
      }
    }

    if (this.typeFilter) {
      const selectedType = this.availableTypes.find(t => t.rtyId === this.typeFilter);
      if (selectedType) {
        filter.RequestTypeId = selectedType.rtyId;
      }
    }

    // For search text, use JsonProperty and JsonValue (only if 3+ characters)
    if (this.searchText && this.searchText.length >= this.minSearchLength) {
      filter.JsonProperty = 'data'; // Search in data property
      filter.JsonValue = this.searchText;
    }

    // Emit the filter to parent component
    this.filtersChanged.emit(filter);
  }

  clearFilters(): void {
    this.statusFilter = '';
    this.typeFilter = '';
    this.searchText = '';
    this.filtersCleared.emit();
  }
}