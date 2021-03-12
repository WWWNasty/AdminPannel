interface TypeObjectProperties extends SelectOption {
    questionaryObjectTypeId: number;
}

const FormDialogObject = (props) => {
    const dialogForm = useForm();
    const form = useFormContext();
    const selectedObjectTypeId = form.watch('objectTypeId');
    const {register, handleSubmit, control, reset, errors, setError} = dialogForm;

    const onSubmit = async data => {
        data.objectTypeId = selectedObjectTypeId;

        data.selectedObjectPropertyValues?.forEach(prop => prop.objectPropertyId = Number(prop.objectPropertyId));

        const response = await fetch("/api/QuestionaryObjectApi", {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            credentials: "include",
            body: JSON.stringify(data)
        })

        if (response.ok) {
            const result = await response.json();
            props.setOpenAlertGreen(true);
            debugger;
            props.selectedObjectype.questionaryObjects.push(result);
            setOpen(false);
        } else if (response.status == 400) {
            const type = 'oneOrMoreRequired';
            setError('code', {type, message: 'Введите уникальный код!'});
        } else {
            props.setOpenAlertRed(true);
        }

    };
    const [open, setOpen] = React.useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
        reset();
    };

    return (
        <div>
            <Button variant="outlined" color="primary" onClick={handleClickOpen} className="mt-3 mb-2">
                Создать новый объект
            </Button>
            <Dialog fullWidth={true} maxWidth={'lg'} open={open} onClose={handleClose}
                    aria-labelledby="form-dialog-title">
                <DialogTitle id="form-dialog-title">Новый объект</DialogTitle>
                <form autoComplete="off">
                    <DialogContent>
                        <Controller
                            as={TextField}
                            autoFocus
                            name="name"
                            control={control}
                            defaultValue=""
                            required
                            margin="dense"
                            id="standard-required"
                            label="Название объекта"
                            fullWidth={true}
                        />

                        <Controller
                            error={errors?.code?.type}
                            as={TextField}
                            autoFocus
                            name="code"
                            control={control}
                            defaultValue=""
                            required
                            margin="dense"
                            id="standard-required"
                            label="Код"
                            fullWidth={true}
                            rules={{required: true, maxLength: {message:'Максимально символов: 20', value:250}, validate: true}}
                            helperText={errors?.code?.message}
                        />
                        
                        {/*<FormControl*/}
                        {/*    {...props}*/}
                        {/*    error={errors?.code?.type}*/}
                        {/*    fullWidth={true}*/}
                        {/*    rules={{required: true, maxLength: {message:'Максимально символов: 20', value:20}, validate: true}}*/}
                        {/*>*/}
                        {/*    <Controller*/}
                        {/*        as={TextField}*/}
                        {/*        name="code"*/}
                        {/*        control={control}*/}
                        {/*        defaultValue=""*/}
                        {/*        required*/}
                        {/*        margin="dense"*/}
                        {/*        id="standard-required"*/}
                        {/*        label="Код"*/}
                        {/*    />*/}
                        {/*    <FormHelperText>{errors.code?.message}</FormHelperText>*/}
                        {/*</FormControl>*/}
                        <Controller
                            as={TextField}
                            name="description"
                            control={control}
                            defaultValue=""
                            required
                            margin="dense"
                            id="standard-multiline-static"
                            label="Описание"
                            fullWidth={true}
                            multiline
                            rows={4}
                        />

                        <div className="mt-3">Заполните свойства типа объекта: </div>
                        {props.selectedObjectype.objectProperties?.map((item, index) =>
                            <div>
                                <Controller
                                    as={TextField}
                                    name={`selectedObjectPropertyValues[${index}].value`}
                                    control={control}
                                    defaultValue=""
                                    required
                                    margin="dense"
                                    id="standard-required"
                                    label={item.name}
                                    fullWidth={true}
                                />
                                <input type="hidden"
                                       ref={register}
                                       name={`selectedObjectPropertyValues[${index}].objectPropertyId`}
                                       value={item.id}
                                />
                            </div>
                        )}

                    </DialogContent>
                    <DialogActions>
                        <Button onClick={handleClose} color="primary">
                            Отмена
                        </Button>
                        <Button onClick={handleSubmit(onSubmit)} color="primary">
                            Добавить
                        </Button>
                    </DialogActions>
                </form>
            </Dialog>
        </div>
    );
}