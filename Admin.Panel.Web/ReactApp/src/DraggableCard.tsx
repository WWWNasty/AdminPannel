const DraggableCard = (props) =>{
    const [state, setState] = React.useState({
        checked: true,
        gilad: true,
        jason: false,
        antoine: false,
    });
    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setState({ ...state, [event.target.name]: event.target.checked });
    };
    const removeFriend = index => () => {
        props.setIndexes(prevIndexes => [...prevIndexes.filter(item => item !== index)]);
        props.setCounter(prevCounter => prevCounter - 1);
    };
    const useStyles = makeStyles((theme: any) =>
        createStyles({
            root: {
                width: '80%',
            },
            heading: {
                fontSize: theme.typography.pxToRem(15),
                fontWeight: theme.typography.fontWeightRegular,
            },
        }),
    );
    const classes = useStyles();
    const { gilad, jason, antoine } = state;
    return(
        <div className="mt-3 bg-light">
            <ListItem
                ContainerComponent="li"
                ContainerProps={{ ref: props.provided.innerRef }}
                {...props.provided.draggableProps}
                {...props.provided.dragHandleProps}
                style={getItemStyle(
                    props.snapshot.isDragging,
                    props.provided.draggableProps.style
                )}
            >
                <ListItemIcon>
                    <Icon>help</Icon>
                </ListItemIcon>
                <div className='col'>
                    <div className='row'>
                        <ListItemText
                            primary= "Вопрос" 
                            //{props.item.primary}
                            // secondary={props.item.secondary}
                        />
                    </div>
                    <div className='row'>
                        <TextField required id="standard-basic" label="Текст вопроса" />
                        <FormControlLabel
                            control={
                                <Switch
                                    checked={state.checked}
                                    onChange={handleChange}
                                    name="checked"
                                    color="primary"
                                />
                            }
                            label="Обязательный вопрос"
                        />
                       
                    </div>
                    <div className={`${classes.root} mt-3`}>
                        <Accordion>
                            <AccordionSummary
                                expandIcon={<Icon>expand_more</Icon>}
                                aria-controls="panel1a-content"
                                id="panel1a-header"
                            >
                                <Typography className={classes.heading}>Ответы</Typography>
                            </AccordionSummary>
                            <AccordionDetails>
                                <Typography>
                                    тут будут ответы

                                    <FormGroup>
                                        <FormControlLabel
                                            control={<Checkbox checked={gilad} onChange={handleChange} color="primary" name="gilad" />}
                                            label="Gilad Gray"
                                        />
                                        <FormControlLabel
                                            control={<Checkbox checked={jason} onChange={handleChange} color="primary" name="jason" />}
                                            label="Jason Killian"
                                        />
                                        <FormControlLabel
                                            control={<Checkbox checked={antoine} onChange={handleChange} color="primary" name="antoine" />}
                                            label="Antoine Llorca"
                                        />
                                    </FormGroup>
                                </Typography>
                            </AccordionDetails>
                        </Accordion>
                    </div> 
                </div>
              
                <ListItemSecondaryAction>
                    <IconButton onClick={removeFriend(props.index)}>
                        <Icon>delete</Icon>
                    </IconButton>
                </ListItemSecondaryAction>
            </ListItem>
        </div>
    );
}