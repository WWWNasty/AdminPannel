interface SelectOption {
    id: number;
    name: string;
}
const MySelect = (props) => {
    const classes = useStyles();

    const handleChange = (event) => {
        props.setSelectedValue(event.target.value);
    };
    
    return (
        <div>
            <FormControl required className={`${classes.formControl} col-md-3`}>
                <InputLabel id="demo-simple-select-required-label">{props.nameSwlect}</InputLabel>
                <Select
                    labelId="demo-simple-select-required-label"
                    id="demo-simple-select-required"
                    value={props.selectedValue}
                    onChange={handleChange}
                    className={classes.selectEmpty}
                > 
                    {props.selectOptions.map((item) => <MenuItem value={item.id}>{item.name}</MenuItem>)}
                </Select>
                <FormHelperText>Обязательное</FormHelperText>
            </FormControl>
        </div>
    );
}