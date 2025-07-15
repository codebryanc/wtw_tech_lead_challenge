import { Component, OnInit, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { RequestService } from '../../service/remote/request.service';
import { RequestsEntity } from '../../models/requestsEntity';
import { RequestCardComponent } from '../request-card/request-card.component';
import { RequestTypeService } from '../../service/remote/request-type.service';
import { RequestStatusService } from '../../service/remote/request-status.service';
import { RequestStatusEntity } from '../../models/requestStatusEntity';
import { RequestTypeEntity } from '../../models/requestTypeEntity';

@Component({
  selector: 'app-all-request',
  standalone: true,
  imports: [
    CommonModule,
    RequestCardComponent,
    FormsModule
  ],
  templateUrl: './all-request.component.html',
  styleUrl: './all-request.component.scss'
})
export class AllRequestComponent implements OnInit {

  requests: RequestsEntity[] = [];
  loading: boolean = false;
  error: string | null = null;
  
  // Filter properties for UI only
  statusFilter: string = '';
  typeFilter: string = '';
  searchText: string = '';
  
  // Available options for filters
  availableStatuses: RequestStatusEntity[] = [];
  availableTypes: RequestTypeEntity[] = [];

  constructor(
    private _requestService: RequestService,
    private _requestTypeService : RequestTypeService,
    private _requestStatusService : RequestStatusService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.loadAllRequests();
      this.getFilterOptions();
    }
  }

  loadAllRequests(): void {
    this.loading = true;
    this.error = null;
    
    this._requestService.getAllRequests().subscribe({
      next: (data) => {
        this.requests = data || [];
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error loading requests: ' + (err.message || 'Unknown error');
        this.loading = false;
        this.requests = [];
      }
    });
  }

  refreshRequests(): void {
    this.loadAllRequests();
  }

  trackByRequestId(index: number, request: RequestsEntity): string {
    return request.reqId;
  }

  private getFilterOptions(): void {
    this.availableStatuses = this._requestStatusService.getRequestStatus();
    this.availableTypes = this._requestTypeService.getRequestTypes();
  }

  onFilterChange(): void {
    
  }

  clearFilters(): void {
    this.statusFilter = '';
    this.typeFilter = '';
    this.searchText = '';
  }
}
