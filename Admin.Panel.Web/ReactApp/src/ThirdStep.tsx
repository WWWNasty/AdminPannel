const ThirdStep = () => {
    const {register, handleSubmit} = useForm();

    const onSubmit = data => {
        console.log(data);
    };


    return (
        <div>
            <form onSubmit={handleSubmit(onSubmit)}>
                <TextField
                    required
                    autoFocus
                    margin="dense"
                    id="standard-required"
                    label="Название"
                    fullWidth
                />
                <DraggableComponent/>
            </form>
        </div>
    );
}
