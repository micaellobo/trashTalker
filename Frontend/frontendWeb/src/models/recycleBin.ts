import { container } from "./container";

export class recycleBin{
    _id:string;
    status: string;
    longit: Number;
    latit: Number;
    street: string;
    city: string;
    zipCode: string;
    country: string;
    containers: container[]
    constructor( _id:string,status:string,longit:Number,latit:Number,
        street:string,city:string,zipCode:string,country:string,containersDtos: container[]){
            this._id=_id;
            this.status= status;
            this.longit= longit;
            this.latit= latit;
            this.street= street;
            this.city= city;
            this.zipCode= zipCode;
            this.country= country;
            this.containers = containersDtos
    }
}