export default class TargetAudience {
	Id: number;
	ProjectId: number;
	Name: string;
	EstimatedIR: number | null;
	EstimatedLOI: number | null;
	Limit: number | null;

	constructor() {
		this.Id = -1;
		this.ProjectId = -1;
		this.Name = '';
		this.EstimatedIR = null;
		this.EstimatedLOI = null;
		this.Limit = null;
	}
}
