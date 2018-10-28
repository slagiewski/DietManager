﻿//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

using LinqToDB;
using LinqToDB.Mapping;

namespace DM.Database
{
	/// <summary>
	/// Database       : DietManager
	/// Data Source    : tcp://localhost:5432
	/// Server Version : 9.5.14
	/// </summary>
	public partial class DietManagerDB : LinqToDB.Data.DataConnection
	{
		public ITable<Achievement>                  Achievements                  { get { return this.GetTable<Achievement>(); } }
		public ITable<Favourite>                    Favourites                    { get { return this.GetTable<Favourite>(); } }
		public ITable<Friend>                       Friends                       { get { return this.GetTable<Friend>(); } }
		public ITable<Image>                        Images                        { get { return this.GetTable<Image>(); } }
		public ITable<Meal>                         Meals                         { get { return this.GetTable<Meal>(); } }
		public ITable<MealFullMealIngredient>       MealFullMealIngredients       { get { return this.GetTable<MealFullMealIngredient>(); } }
		public ITable<MealIngredient>               MealIngredients               { get { return this.GetTable<MealIngredient>(); } }
		public ITable<MealIngredientsWithNutrition> MealIngredientsWithNutritions { get { return this.GetTable<MealIngredientsWithNutrition>(); } }
		public ITable<MealMealIngredient>           MealMealIngredients           { get { return this.GetTable<MealMealIngredient>(); } }
		public ITable<MealScheduleEntry>            MealScheduleEntries           { get { return this.GetTable<MealScheduleEntry>(); } }
		public ITable<Nutrition>                    Nutritions                    { get { return this.GetTable<Nutrition>(); } }
		public ITable<Role>                         Roles                         { get { return this.GetTable<Role>(); } }
		public ITable<User>                         Users                         { get { return this.GetTable<User>(); } }
		public ITable<UserActivity>                 UserActivities                { get { return this.GetTable<UserActivity>(); } }

		public DietManagerDB()
		{
			InitDataContext();
		}

		public DietManagerDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
		}

		partial void InitDataContext();
	}

	[Table(Schema="Socials", Name="Achievement")]
	public partial class Achievement
	{
		[PrimaryKey, NotNull] public Guid   UserId           { get; set; } // uuid
		[Column,     NotNull] public string AchievementType  { get; set; } // text
		[Column,     NotNull] public int    AchievementValue { get; set; } // integer

		#region Associations

		/// <summary>
		/// fk_achievement_user
		/// </summary>
		[Association(ThisKey="UserId", OtherKey="Id", CanBeNull=false, Relationship=Relationship.OneToOne, KeyName="fk_achievement_user", BackReferenceName="fkachievementuser")]
		public User User { get; set; }

		#endregion
	}

	[Table(Schema="Meals", Name="Favourites")]
	public partial class Favourite
	{
		[PrimaryKey, NotNull] public Guid Id     { get; set; } // uuid
		[Column,     NotNull] public Guid MealId { get; set; } // uuid
		[Column,     NotNull] public Guid UserId { get; set; } // uuid

		#region Associations

		/// <summary>
		/// fk_favourites_mealid
		/// </summary>
		[Association(ThisKey="MealId", OtherKey="Id", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="fk_favourites_mealid", BackReferenceName="fkfavouritesmealids")]
		public Meal Meal { get; set; }

		/// <summary>
		/// fk_favourites_userid
		/// </summary>
		[Association(ThisKey="UserId", OtherKey="Id", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="fk_favourites_userid", BackReferenceName="fkfavouritesuserids")]
		public User User { get; set; }

		#endregion
	}

	[Table(Schema="Socials", Name="Friend")]
	public partial class Friend
	{
		[PrimaryKey(1), NotNull] public Guid           User1Id      { get; set; } // uuid
		[PrimaryKey(2), NotNull] public Guid           User2Id      { get; set; } // uuid
		[Column,        NotNull] public bool           Confirmed    { get; set; } // boolean
		[Column,        NotNull] public DateTimeOffset CreationDate { get; set; } // timestamp (6) with time zone

		#region Associations

		/// <summary>
		/// fk_friend_user1
		/// </summary>
		[Association(ThisKey="User1Id", OtherKey="Id", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="fk_friend_user1", BackReferenceName="fk_friend_user1_BackReferences")]
		public User User1 { get; set; }

		/// <summary>
		/// fk_friend_user2
		/// </summary>
		[Association(ThisKey="User2Id", OtherKey="Id", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="fk_friend_user2", BackReferenceName="fkfriendusers")]
		public User User2 { get; set; }

		#endregion
	}

	[Table(Schema="Images", Name="Image")]
	public partial class Image
	{
		[PrimaryKey, NotNull] public Guid   Id   { get; set; } // uuid
		[Column,     NotNull] public string Path { get; set; } // text

		#region Associations

		/// <summary>
		/// fk_user_image_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="ImageId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<User> fkuserimages { get; set; }

		/// <summary>
		/// FK_MealIngredient_Image_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="ImageId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<MealIngredient> MealIngredients { get; set; }

		/// <summary>
		/// FK_Meal_Image_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="ImageId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Meal> Meals { get; set; }

		#endregion
	}

	[Table(Schema="Meals", Name="Meal")]
	public partial class Meal
	{
		[PrimaryKey, NotNull    ] public Guid           Id           { get; set; } // uuid
		[Column,     NotNull    ] public DateTimeOffset CreationDate { get; set; } // timestamp (6) with time zone
		[Column,        Nullable] public Guid?          CreatorId    { get; set; } // uuid
		[Column,        Nullable] public Guid?          ImageId      { get; set; } // uuid
		[Column,     NotNull    ] public string         Name         { get; set; } // text
		[Column,        Nullable] public string         Description  { get; set; } // text
		[Column,     NotNull    ] public float          Calories     { get; set; } // real
		[Column,     NotNull    ] public int            NumberOfUses { get; set; } // integer

		#region Associations

		/// <summary>
		/// FK_Meal_User
		/// </summary>
		[Association(ThisKey="CreatorId", OtherKey="Id", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="FK_Meal_User", BackReferenceName="Meals")]
		public User Creator { get; set; }

		/// <summary>
		/// fk_favourites_mealid_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="MealId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Favourite> fkfavouritesmealids { get; set; }

		/// <summary>
		/// fk_usermeal_mealid_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="MealId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<MealScheduleEntry> fkusermealmealids { get; set; }

		/// <summary>
		/// FK_Meal_Image
		/// </summary>
		[Association(ThisKey="ImageId", OtherKey="Id", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="FK_Meal_Image", BackReferenceName="Meals")]
		public Image Image { get; set; }

		/// <summary>
		/// FK_MealMealIngredient_Meal_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="MealId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<MealMealIngredient> MealMealIngredients { get; set; }

		#endregion
	}

	[Table(Schema="Meals", Name="Meal-FullMealIngredient", IsView=true)]
	public partial class MealFullMealIngredient
	{
		[Column(SkipOnUpdate=true), Nullable] public Guid?  MealId                 { get; set; } // uuid
		[Column(SkipOnUpdate=true), Nullable] public int?   Quantity               { get; set; } // integer
		[Column(SkipOnUpdate=true), Nullable] public Guid?  MealIngredientId       { get; set; } // uuid
		[Column(SkipOnUpdate=true), Nullable] public string MealIngredientName     { get; set; } // text
		[Column(SkipOnUpdate=true), Nullable] public Guid?  MealIngredientImageId  { get; set; } // uuid
		[Column(SkipOnUpdate=true), Nullable] public int?   MealIngredientCalories { get; set; } // integer
		[Column(SkipOnUpdate=true), Nullable] public float? Protein                { get; set; } // real
		[Column(SkipOnUpdate=true), Nullable] public float? Carbohydrates          { get; set; } // real
		[Column(SkipOnUpdate=true), Nullable] public float? Fats                   { get; set; } // real
		[Column(SkipOnUpdate=true), Nullable] public float? VitaminA               { get; set; } // real
		[Column(SkipOnUpdate=true), Nullable] public float? VitaminC               { get; set; } // real
		[Column(SkipOnUpdate=true), Nullable] public float? VitaminB6              { get; set; } // real
		[Column(SkipOnUpdate=true), Nullable] public float? VitaminD               { get; set; } // real
	}

	[Table(Schema="Meals", Name="MealIngredient")]
	public partial class MealIngredient
	{
		[PrimaryKey, NotNull    ] public Guid   Id           { get; set; } // uuid
		[Column,        Nullable] public Guid?  ImageId      { get; set; } // uuid
		[Column,        Nullable] public string Name         { get; set; } // text
		[Column,     NotNull    ] public int    Calories     { get; set; } // integer
		[Column,     NotNull    ] public Guid   NutritionsId { get; set; } // uuid

		#region Associations

		/// <summary>
		/// FK_MealIngredient_Image
		/// </summary>
		[Association(ThisKey="ImageId", OtherKey="Id", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="FK_MealIngredient_Image", BackReferenceName="MealIngredients")]
		public Image Image { get; set; }

		/// <summary>
		/// FK_MealMealIngredient_MealIngredient-_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="MealIngredientId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<MealMealIngredient> MealMealIngredients { get; set; }

		/// <summary>
		/// FK_MealIngredient_Nutritions
		/// </summary>
		[Association(ThisKey="NutritionsId", OtherKey="Id", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="FK_MealIngredient_Nutritions", BackReferenceName="MealIngredients")]
		public Nutrition Nutrition { get; set; }

		#endregion
	}

	[Table(Schema="Meals", Name="MealIngredientsWithNutritions", IsView=true)]
	public partial class MealIngredientsWithNutrition
	{
		[Column(SkipOnUpdate=true), Nullable] public Guid?  Id            { get; set; } // uuid
		[Column(SkipOnUpdate=true), Nullable] public string Name          { get; set; } // text
		[Column(SkipOnUpdate=true), Nullable] public Guid?  ImageId       { get; set; } // uuid
		[Column(SkipOnUpdate=true), Nullable] public int?   Calories      { get; set; } // integer
		[Column(SkipOnUpdate=true), Nullable] public float? Protein       { get; set; } // real
		[Column(SkipOnUpdate=true), Nullable] public float? Carbohydrates { get; set; } // real
		[Column(SkipOnUpdate=true), Nullable] public float? Fats          { get; set; } // real
		[Column(SkipOnUpdate=true), Nullable] public float? VitaminA      { get; set; } // real
		[Column(SkipOnUpdate=true), Nullable] public float? VitaminC      { get; set; } // real
		[Column(SkipOnUpdate=true), Nullable] public float? VitaminB6     { get; set; } // real
		[Column(SkipOnUpdate=true), Nullable] public float? VitaminD      { get; set; } // real
	}

	[Table(Schema="Meals", Name="Meal-MealIngredient")]
	public partial class MealMealIngredient
	{
		[PrimaryKey, NotNull] public Guid Id               { get; set; } // uuid
		[Column,     NotNull] public Guid MealIngredientId { get; set; } // uuid
		[Column,     NotNull] public Guid MealId           { get; set; } // uuid
		[Column,     NotNull] public int  Quantity         { get; set; } // integer

		#region Associations

		/// <summary>
		/// FK_MealMealIngredient_Meal
		/// </summary>
		[Association(ThisKey="MealId", OtherKey="Id", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="FK_MealMealIngredient_Meal", BackReferenceName="MealMealIngredients")]
		public Meal Meal { get; set; }

		/// <summary>
		/// FK_MealMealIngredient_MealIngredient-
		/// </summary>
		[Association(ThisKey="MealIngredientId", OtherKey="Id", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="FK_MealMealIngredient_MealIngredient-", BackReferenceName="MealMealIngredients")]
		public MealIngredient MealIngredient { get; set; }

		#endregion
	}

	[Table(Schema="Meals", Name="MealScheduleEntry")]
	public partial class MealScheduleEntry
	{
		[PrimaryKey, NotNull] public Guid           Id     { get; set; } // uuid
		[Column,     NotNull] public Guid           UserId { get; set; } // uuid
		[Column,     NotNull] public Guid           MealId { get; set; } // uuid
		[Column,     NotNull] public DateTimeOffset Date   { get; set; } // timestamp (6) with time zone

		#region Associations

		/// <summary>
		/// fk_usermeal_mealid
		/// </summary>
		[Association(ThisKey="MealId", OtherKey="Id", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="fk_usermeal_mealid", BackReferenceName="fkusermealmealids")]
		public Meal Meal { get; set; }

		/// <summary>
		/// fk_usermeal_userid
		/// </summary>
		[Association(ThisKey="UserId", OtherKey="Id", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="fk_usermeal_userid", BackReferenceName="fkusermealuserids")]
		public User User { get; set; }

		#endregion
	}

	[Table(Schema="Meals", Name="Nutritions")]
	public partial class Nutrition
	{
		[PrimaryKey, NotNull    ] public Guid   Id            { get; set; } // uuid
		[Column,     NotNull    ] public float  Protein       { get; set; } // real
		[Column,     NotNull    ] public float  Carbohydrates { get; set; } // real
		[Column,     NotNull    ] public float  Fats          { get; set; } // real
		[Column,        Nullable] public float? VitaminA      { get; set; } // real
		[Column,        Nullable] public float? VitaminC      { get; set; } // real
		[Column,        Nullable] public float? VitaminB6     { get; set; } // real
		[Column,        Nullable] public float? VitaminD      { get; set; } // real

		#region Associations

		/// <summary>
		/// FK_MealIngredient_Nutritions_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="NutritionsId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<MealIngredient> MealIngredients { get; set; }

		#endregion
	}

	[Table(Schema="Users", Name="Role")]
	public partial class Role
	{
		[PrimaryKey, NotNull] public Guid   Id       { get; set; } // uuid
		[Column,     NotNull] public string RoleName { get; set; } // text

		#region Associations

		/// <summary>
		/// fk_user_role_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="RoleId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<User> fkuserroles { get; set; }

		#endregion
	}

	[Table(Schema="Users", Name="User")]
	public partial class User
	{
		[PrimaryKey, NotNull    ] public Guid           Id           { get; set; } // uuid
		[Column,        Nullable] public string         Email        { get; set; } // character varying(254)
		[Column,        Nullable] public string         UserName     { get; set; } // character varying(20)
		[Column,     NotNull    ] public string         Name         { get; set; } // character varying(20)
		[Column,     NotNull    ] public string         Surname      { get; set; } // character varying(35)
		[Column,     NotNull    ] public string         FullName     { get; set; } // character varying(56)
		[Column,     NotNull    ] public string         Password     { get; set; } // text
		[Column,     NotNull    ] public DateTimeOffset CreationDate { get; set; } // timestamp (6) with time zone
		[Column,        Nullable] public Guid?          ImageId      { get; set; } // uuid
		[Column,     NotNull    ] public Guid           RoleId       { get; set; } // uuid

		#region Associations

		/// <summary>
		/// fk_friend_user1_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="User1Id", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Friend> fk_friend_user1_BackReferences { get; set; }

		/// <summary>
		/// fk_achievement_user_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="UserId", CanBeNull=true, Relationship=Relationship.OneToOne, IsBackReference=true)]
		public Achievement fkachievementuser { get; set; }

		/// <summary>
		/// fk_favourites_userid_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="UserId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Favourite> fkfavouritesuserids { get; set; }

		/// <summary>
		/// fk_friend_user2_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="User2Id", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Friend> fkfriendusers { get; set; }

		/// <summary>
		/// fk_useractivity_user_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="UserId", CanBeNull=true, Relationship=Relationship.OneToOne, IsBackReference=true)]
		public UserActivity fkuseractivityuser { get; set; }

		/// <summary>
		/// fk_usermeal_userid_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="UserId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<MealScheduleEntry> fkusermealuserids { get; set; }

		/// <summary>
		/// fk_user_image
		/// </summary>
		[Association(ThisKey="ImageId", OtherKey="Id", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="fk_user_image", BackReferenceName="fkuserimages")]
		public Image Image { get; set; }

		/// <summary>
		/// FK_Meal_User_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="CreatorId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
		public IEnumerable<Meal> Meals { get; set; }

		/// <summary>
		/// fk_user_role
		/// </summary>
		[Association(ThisKey="RoleId", OtherKey="Id", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="fk_user_role", BackReferenceName="fkuserroles")]
		public Role Role { get; set; }

		#endregion
	}

	[Table(Schema="Socials", Name="UserActivity")]
	public partial class UserActivity
	{
		[PrimaryKey, NotNull] public Guid           UserId       { get; set; } // uuid
		[Column,     NotNull] public string         ActivityType { get; set; } // text
		[Column,     NotNull] public Guid           ContentId    { get; set; } // uuid
		[Column,     NotNull] public DateTimeOffset ActivityDate { get; set; } // timestamp (6) with time zone

		#region Associations

		/// <summary>
		/// fk_useractivity_user
		/// </summary>
		[Association(ThisKey="UserId", OtherKey="Id", CanBeNull=false, Relationship=Relationship.OneToOne, KeyName="fk_useractivity_user", BackReferenceName="fkuseractivityuser")]
		public User User { get; set; }

		#endregion
	}

	public static partial class TableExtensions
	{
		public static Achievement Find(this ITable<Achievement> table, Guid UserId)
		{
			return table.FirstOrDefault(t =>
				t.UserId == UserId);
		}

		public static Favourite Find(this ITable<Favourite> table, Guid Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Friend Find(this ITable<Friend> table, Guid User1Id, Guid User2Id)
		{
			return table.FirstOrDefault(t =>
				t.User1Id == User1Id &&
				t.User2Id == User2Id);
		}

		public static Image Find(this ITable<Image> table, Guid Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Meal Find(this ITable<Meal> table, Guid Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static MealIngredient Find(this ITable<MealIngredient> table, Guid Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static MealMealIngredient Find(this ITable<MealMealIngredient> table, Guid Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static MealScheduleEntry Find(this ITable<MealScheduleEntry> table, Guid Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Nutrition Find(this ITable<Nutrition> table, Guid Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Role Find(this ITable<Role> table, Guid Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static User Find(this ITable<User> table, Guid Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UserActivity Find(this ITable<UserActivity> table, Guid UserId)
		{
			return table.FirstOrDefault(t =>
				t.UserId == UserId);
		}
	}
}
