export interface RequestFilterDto {
  rtyId?: string;
  resId?: string;
  createdFrom?: Date;
  createdTo?: Date;
  data?: string;
  RequestTypeId?: string;
  RequestStatusId?: string;
  FromDate?: Date;
  ToDate?: Date;
  JsonProperty?: string;
  JsonValue?: string;
}

export interface RequestCreateDto {
  rtyId: string;
  resId: string;
  data?: string;
}