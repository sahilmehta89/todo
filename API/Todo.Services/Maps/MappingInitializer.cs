using AutoMapper;

namespace Todo.Services.Maps
{
    public static class MappingInitializer
    {
        public static IMapper Intialize()
        {
            // Auto Mapper Configurations  
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TodoItemProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            return mapper;
        }
    }
}
