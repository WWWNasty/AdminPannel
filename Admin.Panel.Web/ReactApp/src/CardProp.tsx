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
                    as={TextField}
                    name={`objectProperties[${props.index}].name`}
                    className="mr-3 col-md-3"
                    defaultValue=""
                    required
                    control={props.form.control}
                    label="Название свойства"
                />
                <Controller
                    as={TextField}
                    name={`objectProperties[${props.index}].nameInReport`}
                    className="mr-3 col-md-3"
                    defaultValue=""
                    required
                    control={props.form.control}
                    label="Название свойства в отчете"
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
