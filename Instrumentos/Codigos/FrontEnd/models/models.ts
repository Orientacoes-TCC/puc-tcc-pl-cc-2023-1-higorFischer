
export interface ProjectAnalysis {
	totalLines: number;
	totalClasses: number;
	totalMethods: number;
	filesWithBadSmells: number;
	totalBadSmells: number;
	totalVerifiedFiles: number;
}

export interface BadSmell {
	name: string;
	description: string;
	parameters: { [key: string]: string };
}

export interface CodeMethodInfo {
	name: string;
	line: number;
	lines: number;
	parameters: number;
}

export interface CodePropertyInfo {
	name: string;
	line: number;
}

export interface CodeAnalysis {
	name: string;
	lines: number;
	methods: CodeMethodInfo[];
	properties: CodePropertyInfo[];
	badSmells: BadSmell[];
}

export interface CodeConfig {
	longMethod: number;
	longParametersList: number;
	tooManyMethods: number;
	tooManyProperties: number;
	largeClass: number;
}