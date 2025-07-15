export class RequestsEntity {
  reqId: string;
  rtyId: string;
  resId: string;
  createdAt: Date;
  data?: string;
  requestStatusName?: string;
  requestTypeName?: string;
  
  constructor(
    reqId: string,
    rtyId: string,
    resId: string,
    createdAt: Date,
    data?: string,
    requestStatusName?: string,
    requestTypeName?: string,
  ) {
    this.reqId = reqId;
    this.rtyId = rtyId;
    this.resId = resId;
    this.createdAt = createdAt;
    this.data = data;
    this.requestStatusName = requestStatusName;
    this.requestTypeName = requestTypeName;
  }
}