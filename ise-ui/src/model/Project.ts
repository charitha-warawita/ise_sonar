export class Project {
	Id: number;
	Name: string;
	MaconomyNumber: string;
	Owner: string;
	CreationDate: Date;
	LastActivity: Date;

	constructor() {
		this.Id = -1;
		this.Name = '';
		this.MaconomyNumber = '';
		this.Owner = '';
		this.CreationDate = new Date();
		this.LastActivity = new Date();
	}
}
