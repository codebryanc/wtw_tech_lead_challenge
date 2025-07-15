export interface RequestFilterDto {  
  RequestTypeId?: string;
  RequestStatusId?: string;
  FromDate?: Date;
  ToDate?: Date;
  JsonProperty?: string;
  JsonValue?: string;
}

export interface RequestCreateDto {
  RequestTypeId?: string;
  RequestStatusId?: string;
  DynamicData?: any;
}