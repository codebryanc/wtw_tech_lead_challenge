export class RequestsEntity {
  reqId: string;
  rtyId: string;
  resId: string;
  createdAt: Date;
  data?: string;

  constructor(
    reqId: string,
    rtyId: string,
    resId: string,
    createdAt: Date,
    data?: string
  ) {
    this.reqId = reqId;
    this.rtyId = rtyId;
    this.resId = resId;
    this.createdAt = createdAt;
    this.data = data;
  }
}