export class route {
    _id: String;
    name: String;
    status: String;
    dateCriation: String;
    duration: String;
    dateBegin: String;
    dateEnd: String;
    estimatedDuration: String;
    distanceEstimatedKm: number;
    distanceTravelledKm: number;

    constructor(
        id: String,
        name: String,
        status: String,
        dateCriation: String,
        duration: String,
        dateBegin: String,
        dateEnd: String,
        estimatedDuration: String,
        distanceEstimatedKm: number,
        distanceTravelledKm: number) {
        this._id = id ;
        this.name = name ;
        this.status = status;
        this.dateCriation = dateCriation;
        this.duration = duration;
        this.dateBegin = dateBegin ;
        this.dateEnd = dateEnd;
        this.estimatedDuration = estimatedDuration;
        this.distanceEstimatedKm = distanceEstimatedKm;
        this.distanceTravelledKm = distanceTravelledKm;
    }
}