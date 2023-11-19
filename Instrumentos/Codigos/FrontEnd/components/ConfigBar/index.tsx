import { Button, Stack, TextField, LinearProgress, Typography, Box, Tooltip } from "@mui/material"
import { useEffect, useState } from "react";
import { CodeConfig } from "../../models/models";

interface ConfigBarProps { configs: CodeConfig[], loading: boolean, config: CodeConfig, onClick: any, onSelectAnalysis: (file: string) => void, hints: CodeConfig[] }

export function ConfigBar(props: ConfigBarProps) {
	const { config, configs, onClick, hints, onSelectAnalysis } = props;

	const [selected, setSelected] = useState("")
	const [value, setValue] = useState(config);
	const [visualizing, setVisualizing] = useState(true)


	useEffect(() => {
		setValue(config);
	}, [config])

	const changeItemValue = (event: any, value: string): void => {
		setValue(v => ({ ...v, [value]: +event.target.value }))
	}

	const handleClick = (value: any) => {
		onClick(value)
	}

	const handleSelect = (file: string) => {
		setVisualizing(true);
		setSelected(file);
		onSelectAnalysis(file);
	}

	useEffect(() => {
		if (configs && configs.length > 0)
			handleSelect(configs[0].name);
	}, [configs])


	const handleNew = () => {
		setVisualizing(false);
	}


	const translate = (d: string) => {
		const keys = {
			name: "Name",
			longMethod: "Long Method",
			longParametersList: "Long Parameters List",
			tooManyMethods: "Too Many Methods",
			tooManyProperties: "Too Many Properties",
			largeClass: "Large Class",
		} as { [key: string]: string };
		return keys[d];
	}


	return (
		<div style={{
			minWidth: "20%",
			width: "20%",
			background: "rgba(0,0,0,.05)",
			height: "100vh",
			borderRadius: 8,
			position: "fixed"
		}}>
			<Stack spacing={3} p={2} style={{ position: "sticky", }}>
				<div style={{ textAlign: "center", borderBottom: "1px dashed", paddingBottom: 10 }}>
					<Typography variant="h5">
						C# Bad Smell Finder
						<Tooltip title={
							<Box>
								We analyze your code and found some helping metrics:
								{hints?.map((c, i) => {
									return <Stack mx={1} my={2}>
										{i == 0 && <div style={{ borderBottom: "1px dashed white", marginBottom: 10 }}></div>}
										{Object.keys(c).map(d => {
											//@ts-ignore
											return <div>{translate(d)}: {c[d]}</div>
										})}
										<div style={{ borderBottom: "1px dashed white", marginTop: 10 }}></div>
									</Stack>
								})}
							</Box>
						}>
							<Box>
								?
							</Box>
						</Tooltip>
					</Typography>
				</div>
				<Stack spacing={1}>
					<div>
						<Typography>
							Select your config:
						</Typography>
					</div>
					{configs?.map(c => {
						return <Button
							variant="outlined"
							size="small"
							onClick={() => handleSelect(c.name)}
						>
							{c.name} {selected === c.name ? "*" : ""}
						</Button>
					})}
				</Stack>
				{config ? <Stack spacing={3}>
					<TextField
						disabled={visualizing}
						value={value?.name}
						onChange={(e) => setValue(v => ({ ...v, "name": e.target.value }))}
						id="outlined-basic"
						label="Name"
						variant="outlined"
						size="small"
						fullWidth
						required
						type="text"
					/>
					<TextField
						disabled={visualizing}
						value={value?.largeClass}
						onChange={(e) => changeItemValue(e, "largeClass")}
						id="outlined-basic"
						label="Large Class"
						variant="outlined"
						size="small"
						fullWidth
					/>
					<TextField
						disabled={visualizing}
						value={value?.longMethod}
						onChange={(e) => changeItemValue(e, "longMethod")}
						id="outlined-basic"
						label="Long Method"
						variant="outlined"
						size="small"
						fullWidth
					/>
					<TextField
						disabled={visualizing}
						value={value?.tooManyProperties}
						onChange={(e) => changeItemValue(e, "tooManyProperties")}
						id="outlined-basic"
						label="Too Many Properties"
						variant="outlined"
						size="small"
						fullWidth
					/>
					<TextField
						disabled={visualizing}
						value={value?.tooManyMethods}
						onChange={(e) => changeItemValue(e, "tooManyMethods")}
						id="outlined-basic"
						label="Too Many Methods"
						variant="outlined"
						size="small"
						fullWidth
					/>
					<TextField
						disabled={visualizing}
						value={value?.longParametersList}
						onChange={(e) => changeItemValue(e, "longParametersList")}
						id="outlined-basic"
						label="Long Parameter List"
						variant="outlined"
						size="small"
						fullWidth
					/>
					{!visualizing && <Stack spacing={1}>
						<Button size="small" onClick={() => handleClick(value)} variant="contained">Save</Button>
						<Button size="small" onClick={() => setIsAddingNew(c => false)} variant="contained">Cancel</Button>
					</Stack>}
				</Stack> : null}
				<Button onClick={handleNew} variant="contained">Add New</Button>
			</Stack>
		</div >
	)
}