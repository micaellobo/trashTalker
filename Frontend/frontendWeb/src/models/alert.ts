export class alert{
    _id:String;
    alertStatus:String;
    alertType:String;
    date:Date;
    issue:String;
    employeeId:String;
    constructor(
        _id:String,
        alertStatus:String,
        alertType:String,
        date:Date,
        issue:string,
        employeeId:String
        ){
            this._id=_id;
            this.alertStatus=alertStatus;
            this.alertType=alertType;
            this.issue=issue;
            this.date=date;
            this.employeeId=employeeId;
        }
}


