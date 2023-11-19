import axios from 'axios';
import { useEffect, useState } from 'react'
import { ProjectAnalysisCard } from "../components/ProjectAnalysisCard"
import { ProjectAnalysisStatisticsCard } from "../components/ProjectAnalysisStatisticsCard"

import { CodeAnalysis, CodeConfig, ProjectAnalysis } from '../models/models';
import { CodeAnalysisChart } from "../components/CodeAnalysisChart"
import { CodeAnalysisPieChart } from "../components/CodeAnalysisPieChart"
import { BadSmellListing } from "../components/BadSmellListing"
import { ConfigBar } from "../components/ConfigBar"
import { LinearProgress } from '@mui/material';

interface BadSmellApp {
	projectAnalysis: ProjectAnalysis;
	analyses: CodeAnalysis[];
	config: CodeConfig;
	hints: CodeConfig[];
}

function App() {
	const [items, setItems] = useState({} as BadSmellApp);
	const [configs, setConfigs] = useState<CodeConfig[]>([]);
	const [loading, setLoading] = useState(false);

	const fetchAnalysis = (file: string) => {
		setLoading(true)
		axios.post("https://localhost:7289/read", { name: file }).then(data => {
			setItems(data.data);
		}).finally(() => {
			setLoading(false)
		})
	}

	const fetchConfigs = async () => {
		var configs = await axios.get("https://localhost:7289/all")
		setConfigs(configs.data);
	}

	const handleClick = (config: CodeConfig) => {
		setLoading(true);
		axios.post("https://localhost:7289/add", config)
			.then(data => {
				fetchConfigs()
			})
			.finally(() => {
				setLoading(false);
			})
	}

	useEffect(() => {
		fetchConfigs();
	}, [])

	return <div style={{ display: "flex" }}>
		<ConfigBar
			hints={items.hints}
			configs={configs}
			loading={loading}
			config={items?.config}
			onClick={handleClick}
			onSelectAnalysis={fetchAnalysis}
		/>
		{items.config && (
			<div style={{ display: "flex", flexDirection: "column", width: "100%", padding: 10, marginLeft: "20%" }}>
				{loading && <LinearProgress />}
				<div style={{ width: "100ts%", marginBottom: 10 }}>
					<ProjectAnalysisCard projectAnalysis={items.projectAnalysis} />
				</div>
				<div style={{ width: "100%", marginBottom: 10 }}>
					<ProjectAnalysisStatisticsCard projectAnalysis={items.projectAnalysis} />
				</div>
				<div style={{ display: "flex", }}>
					<div style={{ display: "flex", width: "70%", height: "200px", marginBottom: 10 }}>
						<CodeAnalysisChart codeAnalysis={items.analyses} />
					</div>
					<div style={{ display: "flex", width: "30%", height: "200px", marginBottom: 10, marginLeft: 10 }}>
						<CodeAnalysisPieChart codeAnalysis={items.analyses} />
					</div>
				</div>
				<div style={{ display: "flex", width: "100%" }}>
					<BadSmellListing codeAnalysis={items.analyses} />
				</div>
			</div>
		)}
	</div>
}

export default App
