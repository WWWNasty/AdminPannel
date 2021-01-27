interface QuestionaryInputFieldTypes extends SelectOption {
    selectableAnswersListId: number;
}
interface SelectableAnswers {
    id: number;
    name: string;
    selectableAnswersListId: number;
}
const DraggableCard = (props) =>{
    const form = useFormContext();
    const [state, setState] = React.useState({
        checked: true,
        gilad: true,
        jason: false,
        antoine: false,
    });
    const {register, control} = useFormContext();
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
    const selectedSelectableAnswersListId = form.watch(`questionaryQuestions[${props.index}].selectableAnswersListId`);
    const availableSelectableAnswers = props.selectableAnswers.filter(answer => answer.selectableAnswersListId == selectedSelectableAnswersListId);
    console.log("ответы1" + availableSelectableAnswers);
    const availableQuestionaryInputFieldTypeses = props.questionaryInputFieldTypes.filter(input => input.selectableAnswersListId == selectedSelectableAnswersListId);
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
                        />
                    </div>
                    <div className='row'>
                        <Controller
                            as={TextField}
                            name={`questionaryQuestions[${props.index}].questionText`}
                            className="mr-3 col-md-6"
                            defaultValue=""
                            required
                            control={control}
                            label="Текст вопроса"
                        />
                        <MySelect required
                                  name={`questionaryQuestions[${props.index}].selectableAnswersListId`}
                                  selectOptions={props.selectableAnswersLists}
                                  nameSwlect="Варианты ответа"
                        />
                        <MySelect required
                                  name={`questionaryQuestions[${props.index}].questionaryInputFieldTypeId`}
                                  selectOptions={availableQuestionaryInputFieldTypeses}
                                  nameSwlect="Тип ввода"
                        />
                        <MySelect name={`questionaryQuestions[${props.index}].selectableAnswersListId`}
                                  selectOptions={availableSelectableAnswers}
                                  nameSwlect="Ответ по умолчанию"
                        />
                        <input type="hidden"
                               ref={register}
                               name={`questionaryQuestions[${props.index}].sequenceOrder`}
                               value={props.index}
                        />
                        <FormControlLabel
                            control={
                                <Switch
                                    className="mr-3"
                                    name={`questionaryQuestions[${props.index}].canSkipQuestion`}
                                    color="primary"
                                    inputRef={register}
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
                                    Выберите ниже ответы которым необходим комментарий
                                    <FormGroup>
                                        {availableSelectableAnswers?.map((item, index) =>
                                            <div>
                                                {/*<FormControlLabel*/}
                                                {/*    control={<Checkbox checked={gilad} onChange={handleChange} color="primary" name="gilad" />}*/}
                                                {/*    label="Gilad Gray"*/}
                                                {/*/>*/}

                                                <FormControlLabel
                                                    control={
                                                        <Checkbox
                                                            name={`questionaryQuestions[${props.index}].questionaryAnswerOptions[${index}].isInvolvesComment`}
                                                            color="primary"
                                                            inputRef={register}
                                                        />
                                                    }
                                                    label={item.name}
                                                />
                                                <input type="hidden"
                                                       ref={register}
                                                       name={`questionaryQuestions[${props.index}].questionaryAnswerOptions[${index}].selectableAnswerId`}
                                                       value={item.id}
                                                />
                                                {/*<FormControlLabel*/}
                                                {/*    color="primary"*/}
                                                {/*    value={item.id}*/}
                                                {/*    control={<Checkbox />}*/}
                                                {/*    label={item.name}*/}
                                                {/*    name={`techStack[${item.id}]`}*/}
                                                {/*    inputRef={register}*/}
                                                {/*/>*/}
                                                
                                                {/*<Controller*/}
                                                {/*    as={Checkbox}*/}
                                                {/*    name={`techStack[${item.id}]`}*/}
                                                {/*    control={control}*/}
                                                {/*    defaultValue=""*/}
                                                {/*    label={item.name}*/}
                                                {/*    color="primary"*/}
                                                {/*/>*/}
                                                
                                                {/*<Controller*/}
                                                {/*    as={TextField}*/}
                                                {/*    name={`selectedObjectPropertyValues[${index}].value`}*/}
                                                {/*    control={control}*/}
                                                {/*    defaultValue=""*/}
                                                {/*    required*/}
                                                {/*    margin="dense"*/}
                                                {/*    id="standard-required"*/}
                                                {/*    label={item.name}*/}
                                                {/*    fullWidth={true}*/}
                                                {/*/>*/}
                                                {/*<input type="hidden"*/}
                                                {/*       ref={register}*/}
                                                {/*       name={`selectedObjectPropertyValues[${index}].objectPropertyId`}*/}
                                                {/*       value={item.id}*/}
                                                {/*/>*/}
                                            </div>
                                        )}
                                      
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