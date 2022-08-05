import { addDays, subDays } from 'date-fns';
import Project from '@/model/Project';

// NOTE: This is just consistent, dummy data.
const project = new Project(
	'Project 1',
	'123',
	'John, Doe',
	addDays(new Date(), 15),
	addDays(new Date(), 25)
);
project.Id = 1;
project.CreationDate = subDays(new Date(), 5);
project.LastActivity = subDays(new Date(), 2);

const projects: Project[] = [project];

// TODO: Implement API calls.
export default class ProjectService {
	static async GetAllAsync(): Promise<Project[]> {
		const result = [...projects]; // Avoid returning a reference to projects const.

		return Promise.resolve(result);
	}

	static async GetAsync(id: number): Promise<Project | null> {
		const result = projects.find((p) => p.Id === id) ?? null;

		const p: Project = { ...result } as Project; // Avoid returning a reference to projects const.
		return Promise.resolve(p);
	}

	static async GetByNameAsync(filter: string): Promise<Project[]> {
		const result = projects.filter((p) => p.Name.includes(filter));

		return Promise.resolve(result);
	}

	static async CreateAsync(p: Project): Promise<number> {
		const result = projects.push(p);

		return Promise.resolve(result);
	}

	static async UpdateAsync(p: Project): Promise<void> {
		if (p.Id === -1) return Promise.reject('Project does not have a valid Id.');

		const i = projects.findIndex((x) => x.Id == p.Id);
		if (i === -1) return Promise.reject('Project Not Found');

		const proj = projects[i];
		proj.Name = p.Name;
		proj.MaconomyNumber = p.MaconomyNumber;
		proj.Owner = p.Owner;
		proj.StartDate = p.StartDate;
		proj.EndDate = p.EndDate;
		proj.LastActivity = new Date();

		return Promise.resolve();
	}
}
