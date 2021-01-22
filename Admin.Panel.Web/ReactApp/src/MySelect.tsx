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
                    <Select required labelId={labelId} label={label}>
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
    
    return (

        <FormControl className={`${classes.formControl} col-md-3 mr-3`}>
            <ReactHookFormSelect
                name={props.name}
                label={props.nameSwlect}
                defaultValue={props.selectedValue}
                className={classes.selectEmpty}
                control={props.form.control}
                //error={!!errors.nome}
            >
                {props.selectOptions?.map((item) => <MenuItem value={item.id}>{item.name}</MenuItem>) ?? []}
            </ReactHookFormSelect>
        </FormControl>
    );
}
