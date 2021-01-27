const ThirdStep = (props) => {
    const {register, handleSubmit, control} = useFormContext();

    const onSubmit = data => {
        console.log(data);
    };
    const form = useFormContext();
    return (
        <div>
            <form onSubmit={handleSubmit(onSubmit)}>
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
            </form>
        </div>
    );
}
