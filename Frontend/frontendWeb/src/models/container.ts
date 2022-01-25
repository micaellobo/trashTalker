export class container{
    [x: string]: any;
    _id:String;
    status:String;
    typeOfWaste:String;
    height:number;
    width:number;
    depth:number;
    percentageOccupied:number;
    idRecBin:string
    constructor(id:String,
        status:String,
        typeOfWaste:String,
        height:number,
        width:number,
        depth:number,
        percentageOccupied:number,
        idRecBin:string){
            this._id=id;
            this.status=status;
            this.typeOfWaste=typeOfWaste;
            this.height=height;
            this.width=width;
            this.depth=depth;
            this.percentageOccupied=percentageOccupied;
            this.idRecBin=idRecBin;
        }
}


