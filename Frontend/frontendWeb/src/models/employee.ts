export class employee{
    _id:String;
    username:String;
    password:String;
    role:String;
    firstName:String;
    lastName:String;
    email:String;
    gender:String;
    status:String;
    street:String;
    city:String;
    zipCode:String;
    country:String;

    constructor(_id:String,username:String,
        password:String,role:String,firstName:String,
        lastName:String,email:String,
        gender:String,status:String,
        street:String,city:String,
        zipCode:String,country:String){
            this._id=_id;
            this.username=username;
            this.password=password;
            this.role=role;
            this.firstName=firstName;
            this.lastName=lastName;
            this.email=email;
            this.gender=gender;
            this.status=status;
            this.street=street;
            this.city=city;
            this.zipCode=zipCode;
            this.country=country;
        }
}


