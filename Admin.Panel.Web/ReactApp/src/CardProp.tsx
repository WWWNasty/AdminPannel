function CardProp(props: {index: number, setIndexes: any, setCounter: any, form: any, registerForm: any}) {
    const classes = useStyles();

    const removeFriend = index => () => {
        props.setIndexes(prevIndexes => [...prevIndexes.filter(item => item !== index)]);
        props.setCounter(prevCounter => prevCounter - 1);
    };
    return (
        <Card className={`${classes.root} mt-3 mb-3 bg-light`}>
            <CardContent>
                <Typography className={classes.pos} color="textSecondary">
                    <div className={'d-flex'}>
                        свойство
                        <IconButton aria-label="delete" className={`${classes.margin} ml-auto`} onClick={removeFriend(props.index)}>
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
                    label="Название"
                />
                <Controller
                    as={TextField}
                    name={`objectProperties[${props.index}].nameInReport`}
                    className="mr-3 col-md-3"
                    defaultValue=""
                    required
                    control={props.form.control}
                    margin="dense"
                    label="Название в отчете"
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
