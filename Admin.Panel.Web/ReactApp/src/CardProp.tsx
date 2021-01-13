function CardProp() {
    const classes = useStyles();
    const [state, setState] = React.useState({
        checked: true,
    });
    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setState({ ...state, [event.target.name]: event.target.checked });
    };
    return (
         
        <Card className={`${classes.root} mt-3 mb-3 bg-light`}>
            <CardContent>
                <Typography className={classes.pos} color="textSecondary">
                    <div className={'d-flex'}>
                        свойство
                        <IconButton aria-label="delete" className={`${classes.margin} ml-auto`}>
                            <Icon>delete</Icon>
                        </IconButton>   
                    </div>
                </Typography>
                    <TextField id="standard-basic" label="Название" />
                    <TextField id="standard-basic" label="Название в отчете" />
                <FormControlLabel
                    control={
                        <Checkbox
                            checked={state.checked}
                            onChange={handleChange}
                            name="checkedB"
                            color="primary"
                        />
                    }
                    label="Это поле используется в отчете"
                />
            </CardContent>
        </Card>
    );
}
