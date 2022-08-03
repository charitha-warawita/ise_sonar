import type TargetAudience from './TargetAudience';

export default class Project {
	Id: number;
	Name: string;
	MaconomyNumber: string;
	Owner: string;
	CreationDate: Date;
	LastActivity: Date;
	TargetAudiences: TargetAudience[];

	constructor() {
		this.Id = -1;
		this.Name = '';
		this.MaconomyNumber = '';
		this.Owner = '';
		this.CreationDate = new Date();
		this.LastActivity = new Date();
		this.TargetAudiences = [];
	}
}
