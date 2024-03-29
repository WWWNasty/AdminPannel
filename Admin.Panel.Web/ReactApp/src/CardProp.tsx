function CardProp(props: {index: number, form: any, registerForm: any, remove: any}) {
    const classes = useStyles();
    
    return (
        <Card className={`${classes.root} mt-3 mb-3 bg-light`}>
            <CardContent>
                <Typography className={classes.pos} color="textSecondary">
                    <div className={'d-flex'}>
                        Cвойство
                        <IconButton aria-label="delete" className={`${classes.margin} ml-auto`} onClick={() => props.remove(props.index)}>
                            <Icon>delete</Icon>
                        </IconButton>
                    </div>
                </Typography>
                <Controller
                    error={props.form.errors?.objectProperties?.[props.index]?.name?.type}
                    as={TextField}
                    name={`objectProperties[${props.index}].name`}
                    className="mr-3 col-md-3"
                    defaultValue=""
                    required
                    control={props.form.control}
                    label="Название свойства"
                    rules={{required: true, maxLength: {message:'Максимально символов: 250', value:250}, validate: true}}
                    helperText={props.form.errors?.objectProperties?.[props.index]?.name?.message}
                />
                <Controller
                    error={props.form.errors?.objectProperties?.[props.index]?.nameInReport?.type}
                    as={TextField}
                    name={`objectProperties[${props.index}].nameInReport`}
                    className="mr-3 col-md-3"
                    defaultValue=""
                    required
                    control={props.form.control}
                    label="Название свойства в отчете"
                    rules={{required: true, maxLength: {message:'Максимально символов: 250', value:250}, validate: true}}
                    helperText={props.form.errors?.objectProperties?.[props.index]?.nameInReport?.message}
                />  
                
                <FormControlLabel
                    control={
                        <Switch
                            className="mr-3"
                            name={`objectProperties[${props.index}].isUsedInReport`}
                            color="primary"
                            inputRef={props.registerForm}
                        />
                    }
                    label="Это поле используется в отчете"
                />
            </CardContent>
        </Card>
    );
}
