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
    const labelId = `${name}-label`;
    return (
        <FormControl {...props}>
            <InputLabel id={labelId}>{label}</InputLabel>
            <Controller
                as={
                    <Select labelId={labelId} label={label}>
                        {children}
                    </Select>
                }
                name={name}
                control={control}
                defaultValue={defaultValue}
            />
        </FormControl>
    );
};

const MySelect = (props) => {
    const classes = useStyles();
    const {control} = useFormContext();
    
    return (
        <FormControl className={`${classes.formControl} col-md-3 mr-3`}>
            <ReactHookFormSelect
                required
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
