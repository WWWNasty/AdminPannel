const MyMultipleSelect = (props) => {
    function getStyles(name, personName, theme) {
        return {
            fontWeight:
                personName.indexOf(name) === -1
                    ? theme.typography.fontWeightRegular
                    : theme.typography.fontWeightMedium,
        };
    }
    const useStyles = makeStyles((theme) => ({
        formControl: {
            margin: theme.spacing(0),
            // minWidth: 120,
            // maxWidth: 300,
        },
        chips: {
            display: 'flex',
            flexWrap: 'wrap',
        },
        chip: {
            margin: 2,
        },
        noLabel: {
            marginTop: theme.spacing(3),
        },
    }));

    const ITEM_HEIGHT = 48;
    const ITEM_PADDING_TOP = 8;
    const MenuProps = {
        PaperProps: {
            style: {
                maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
                // width: 250,
            },
        },
    };
    const classes = useStyles();
    const handleChange = (event) => {
        props.setSelectedValue(event.target.value);
    };
    return (
        <div>
            <FormControl className={`${classes.formControl} col-md-3`}>
                <InputLabel id="demo-mutiple-chip-label">Chip</InputLabel>
                <Select
                    labelId="demo-mutiple-chip-label"
                    id="demo-mutiple-chip"
                    multiple
                    value={props.selectedValue}
                    onChange={handleChange}
                    input={<Input id="select-multiple-chip" />}
                    renderValue={(selected) => (
                        <div className={classes.chips}>
                            {props.selectOptions.filter(option => selected.indexOf(option.id) > -1).map((option) => (
                                <Chip key={option.id} label={option.name} className={classes.chip} />
                            ))}
                        </div>
                    )}
                    MenuProps={MenuProps}
                >
                    {props.selectOptions.map((item) =>
                        <MenuItem value={item.id}>
                            <Checkbox checked={props.selectedValue.indexOf(item.id) > -1} />
                            <ListItemText primary={item.name} />
                        </MenuItem>)}

                </Select>
                {/*<ListSubheader>Category 1</ListSubheader>*/}
                {/*<MenuItem value={1}>Option 1</MenuItem>*/}
                {/*<MenuItem value={2}>Option 2</MenuItem>*/}
                {/*<ListSubheader>Category 2</ListSubheader>*/}
                {/*<MenuItem value={3}>Option 3</MenuItem>*/}
                {/*<MenuItem value={4}>Option 4</MenuItem>*/}
            </FormControl>
        </div>
    );
}