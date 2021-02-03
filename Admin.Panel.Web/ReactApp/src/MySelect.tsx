interface SelectOption {
    id: number;
    name: string;
}

const ReactHookFormSelect = ({
                                 name,
                                 label,
                                 control,
                                 defaultValue,
                                 children,
                                 ...props
                             }) => {
    const {errors} = useFormContext();
    const labelId = `${name}-label`;
    return (
        <FormControl {...props} error={errors[name]?.type}>
            <InputLabel id={labelId}>{label}</InputLabel>
            <Controller
                as={
                    <Select renderValue={props.renderValue} multiple={props.multiple} labelId={labelId} label={label}>
                        {children}
                    </Select>
                }
                rules={ {required: props.required, minLength: props.minLength, validate: props.validate}} 
                name={name}
                control={control}
                defaultValue={defaultValue}
            />
            <FormHelperText>{errors[name]?.message}</FormHelperText>
        </FormControl>
    );
};

const MySelect = (props) => {
    const classes = useStyles();
    const {control} = props.form ?? useFormContext();
    
    return (
        <FormControl className={`${classes.formControl} col-md-3 mr-3`}>
            <ReactHookFormSelect
                required={props.required}
                name={props.name}
                label={props.nameSwlect}
                defaultValue={props.selectedValue}
                className={classes.selectEmpty}
                control={control}>
                {props.selectOptions?.map((item) => <MenuItem value={item.id}>{item.name}</MenuItem>) ?? []}
            </ReactHookFormSelect>
        </FormControl>
    );
}
