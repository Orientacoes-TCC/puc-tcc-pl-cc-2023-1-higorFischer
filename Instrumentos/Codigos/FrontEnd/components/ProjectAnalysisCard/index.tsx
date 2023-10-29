import { ProjectAnalysis } from "../../models/models"

function ProjectCard(label: string, value: string | number) {
	return (<div style={{ border: "1px solid rgba(0,0,0,.3)", padding: "10px 20px", borderRadius: 4, textAlign: "center" }}>
		<div style={{ fontSize: "0.7rem", color: "rgba(0,0,0,.6)" }}>
			{label}
		</div>
		<div style={{ fontSize: "1.4rem", fontWeight: "bold" }}>
			{value}
		</div>
	</div>)
}

export function ProjectAnalysisCard(props: { projectAnalysis: ProjectAnalysis }) {
	const { projectAnalysis } = props;
	return (
		<div style={{ width: "100%", background: "rgba(0,0,0,.05)", borderRadius: 8, display: "flex" }}>
			<div style={{ padding: 20, display: "flex", justifyContent: "space-between", width: "100%" }}>
				{ProjectCard("Total Verified Files", projectAnalysis?.totalVerifiedFiles?.toLocaleString('pt-BR'))}
				{ProjectCard("Files With Bad Smell", projectAnalysis?.filesWithBadSmells?.toLocaleString('pt-BR'))}
				{ProjectCard("Total Bad Smells", projectAnalysis?.totalBadSmells?.toLocaleString('pt-BR'))}
				{ProjectCard("Total Lines", projectAnalysis?.totalLines?.toLocaleString('pt-BR'))}
				{ProjectCard("Total Classes", projectAnalysis?.totalClasses?.toLocaleString('pt-BR'))}
				{ProjectCard("Total Methods", projectAnalysis?.totalMethods?.toLocaleString('pt-BR'))}
			</div>
		</div>
	)
}