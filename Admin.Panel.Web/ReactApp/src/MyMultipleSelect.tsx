interface QuestionaryObjecTypes extends SelectOption {
    typeObjectProperties: TypeObjectProperties[];
    questionaryObjects: SelectOption[];
    companyId: number;
}

const MyMultipleSelect = (props: { selectOptions: QuestionaryObjecTypes[], selectedValue: any, selectName: string, form: any }) => {

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
                 maxHeight: 700,
                 //ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
                // width: 250,
            },
        },
    };
    const classes = useStyles();
    const {control} = useFormContext();

    return (
        <div>
            <FormControl className={`${classes.formControl} col-md-3`}>
                <ReactHookFormSelect
                    labelId="demo-mutiple-chip-label"
                    id="demo-mutiple-chip" 
                    multiple
                    validate={ () => {
                        const isValid = props.form.getValues('objectsIdToChangeType')?.length > 0;
                        const type = 'oneOrMoreRequired';

                        if(!isValid)
                            props.form.setError('objectsIdToChangeType', {type, message: 'Viberite adin ili bolshe'});
                        
                        return isValid;
                    }}
                    name="objectsIdToChangeType"
                    label={props.selectName}
                    defaultValue={[]}
                    className={classes.selectEmpty}
                    control={control}
                    input={<Input id="select-multiple-chip"/>}
                    renderValue={(selected) => (
                        <div className={classes.chips}>
                            {props.selectOptions
                                .flatMap(option => option.questionaryObjects)
                                .filter(option => selected.indexOf(option.id) > -1)
                                .map((option) =>
                                    <Chip key={option.id} label={option.name} className={classes.chip}/>
                                )}
                        </div>
                    )}
                    MenuProps={MenuProps}>

                    {props.selectOptions?.map(item => [
                        <ListSubheader>{item.name}</ListSubheader>,
                        item.questionaryObjects?.map((object) =>
                            <MenuItem key={object.id} value={object.id}>
                                <Checkbox />
                                <ListItemText primary={object.name}/>
                            </MenuItem>)
                    ])}                
                </ReactHookFormSelect>
                
                {/*<InputLabel id="demo-mutiple-chip-label">{props.selectName}</InputLabel>*/}
                {/*<Select*/}
                {/*    labelId="demo-mutiple-chip-label"*/}
                {/*    id="demo-mutiple-chip"*/}
                {/*    multiple*/}
                {/*    value={props.selectedValue}*/}
                {/*    onChange={handleChange}*/}
                {/*    input={<Input id="select-multiple-chip"/>}*/}
                {/*    renderValue={(selected) => (*/}
                {/*        <div className={classes.chips}>*/}
                {/*            {props.selectOptions*/}
                {/*                .flatMap(option => option.questionaryObjects)*/}
                {/*                .filter(option => selected.indexOf(option.id) > -1)*/}
                {/*                .map((option) =>*/}
                {/*                    <Chip key={option.id} label={option.name} className={classes.chip}/>*/}
                {/*                )}*/}
                {/*        </div>*/}
                {/*    )}*/}
                {/*    MenuProps={MenuProps}>*/}
                
                {/*    {props.selectOptions?.map(item => [*/}
                {/*        <ListSubheader>{item.name}</ListSubheader>,*/}
                {/*        item.questionaryObjects.map((object) =>*/}
                {/*            <MenuItem key={object.id} value={object.id}>*/}
                {/*                <Checkbox />*/}
                {/*                <ListItemText primary={object.name}/>*/}
                {/*            </MenuItem>)*/}
                {/*    ])}*/}
                {/*    */}
                {/*</Select>*/}
                
            </FormControl>
        </div>
    );
}