import { Button, Stack, TextField, LinearProgress, Typography } from "@mui/material"
import { useEffect, useState } from "react";
import { CodeConfig } from "../../models/models";

export function ConfigBar(props: { loading: boolean, config: CodeConfig, onClick: any }) {
	const { loading, config, onClick } = props;

	const [value, setValue] = useState(config);

	useEffect(() => {
		setValue(config);
	}, [config])

	const changeItemValue = (event: any, value: string): void => {
		setValue(v => ({ ...v, [value]: +event.target.value }))
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
					</Typography>
				</div>
				<TextField
					value={value?.largeClass}
					onChange={(e) => changeItemValue(e, "largeClass")}
					id="outlined-basic"
					label="Large Class"
					variant="outlined"
					size="small"
					fullWidth
				/>
				<TextField
					value={value?.longMethod}
					onChange={(e) => changeItemValue(e, "longMethod")}
					id="outlined-basic"
					label="Long Method"
					variant="outlined"
					size="small"
					fullWidth
				/>
				<TextField
					value={value?.tooManyProperties}
					onChange={(e) => changeItemValue(e, "tooManyProperties")}
					id="outlined-basic"
					label="Too Many Properties"
					variant="outlined"
					size="small"
					fullWidth
				/>
				<TextField
					value={value?.tooManyMethods}
					onChange={(e) => changeItemValue(e, "tooManyMethods")}
					id="outlined-basic"
					label="Too Many Methods"
					variant="outlined"
					size="small"
					fullWidth
				/>
				<TextField
					value={value?.longParametersList}
					onChange={(e) => changeItemValue(e, "longParametersList")}
					id="outlined-basic"
					label="Long Parameter List"
					variant="outlined"
					size="small"
					fullWidth
				/>
				{loading
					? <LinearProgress />
					: <Button onClick={() => onClick(value)} variant="contained">Recalculate</Button>}
			</Stack>
		</div>
	)
}