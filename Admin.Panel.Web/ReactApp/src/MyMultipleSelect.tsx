interface QuestionaryObjecTypes extends SelectOption {
    typeObjectProperties: TypeObjectProperties[];
    questionaryObjects: SelectOption[];
    companyId: number;
}

const MyMultipleSelect = (props: { selectOptions: QuestionaryObjecTypes[], selectedValue: any, selectName: string, form: any }) => {
    const form = useFormContext();
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
                    labelId="demo-mutiple-checkbox-label"
                    id="demo-mutiple-checkbox" 
                    multiple
                    validate={ () => {
                        const isValid = props.form.getValues('objectsIdToChangeType')?.length > 0;
                        const type = 'oneOrMoreRequired';

                        if(!isValid)
                            props.form.setError('objectsIdToChangeType', {type, message: 'Выберите объекты для анкеты!'});
                        
                          return isValid;
                    }}
                    name="objectsIdToChangeType"
                    label={props.selectName}
                    defaultValue={[]}
                    className={classes.selectEmpty}
                    control={control}
                    input={<Input/>}
                    selectOptions={props.selectOptions}
                    renderValue={
                        (selected) => (
                            props.selectOptions
                                .flatMap(option => option.questionaryObjects)
                                .filter(option => selected.indexOf(option?.id) > -1)
                                .map((option) => option.name
                                ).join(', ')
                    )}
                    MenuProps={MenuProps}>

                    {props.selectOptions?.map(item => [ item.questionaryObjects?.length?
                        <ListSubheader>{item.name}</ListSubheader> : null,
                        item.questionaryObjects?.map((object) =>
                            <MenuItem key={object.id} value={object.id}>
                                <Checkbox color="primary"
                                          checked={form.watch('objectsIdToChangeType')?.indexOf(object.id) > -1}/>
                                <ListItemText primary={object.name}/>
                            </MenuItem>)
                    ])}                
                </ReactHookFormSelect>
            </FormControl>
        </div>
    );
}