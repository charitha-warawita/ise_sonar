import type TargetAudience from './TargetAudience';

export default class Project {
	Id = -1;
	Name: string;
	MaconomyNumber: string;
	Owner: string;
	CreationDate: Date = new Date();
	LastActivity: Date = new Date();
	StartDate: Date;
	EndDate: Date;
	TargetAudiences: TargetAudience[] = [];

	constructor(name: string, maconomyNumber: string, owner: string, start: Date, end: Date) {
		this.Name = name;
		this.MaconomyNumber = maconomyNumber;
		this.Owner = owner;
		this.StartDate = start;
		this.EndDate = end;
	}
}
