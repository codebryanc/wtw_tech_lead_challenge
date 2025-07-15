export interface RequestFilterDto {
  rtyId?: string;
  resId?: string;
  createdFrom?: Date;
  createdTo?: Date;
  data?: string;
}

export interface RequestCreateDto {
  rtyId: string;
  resId: string;
  data?: string;
}