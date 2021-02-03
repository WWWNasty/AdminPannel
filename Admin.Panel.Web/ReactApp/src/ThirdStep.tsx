const ThirdStep = (props) => {
    const form = useFormContext();
    console.log(form);
    const {register, handleSubmit, control} = form;

    return (
        <div>
            <Controller
                as={TextField}
                autoFocus
                name="name"
                control={control}
                defaultValue=""
                required
                margin="dense"
                id="standard-required"
                label="Название"
                fullWidth={true}
            />
            <DraggableComponent
                form={form}
                selectableAnswersLists={props.selectableAnswersLists}
                questionaryInputFieldTypes={props.questionaryInputFieldTypes}
                selectableAnswers={props.selectableAnswers}
            />
        </div>
    );
}
