﻿using System;
using UnityEngine;

namespace RogueLibsCore
{
	/// <summary>
	///   <para>Repesents a <see cref="CustomItem"/> builder, that attaches additional information to the item.</para>
	/// </summary>
	public class ItemBuilder
	{
		/// <summary>
		///   <para>Initializes a new instance of the <see cref="ItemBuilder"/> class with the specified <paramref name="info"/>.</para>
		/// </summary>
		/// <param name="info">The item metadata to use.</param>
		public ItemBuilder(ItemInfo info) => Info = info;
		/// <summary>
		///   <para>The used item metadata.</para>
		/// </summary>
		public ItemInfo Info { get; }

		/// <summary>
		///   <para>Gets the item's localizable name.</para>
		/// </summary>
		public CustomName Name { get; private set; }
		/// <summary>
		///   <para>Gets the item's localizable description.</para>
		/// </summary>
		public CustomName Description { get; private set; }
		/// <summary>
		///   <para>Gets the item's sprite.</para>
		/// </summary>
		public RogueSprite Sprite { get; private set; }
		/// <summary>
		///   <para>Gets the item's unlock.</para>
		/// </summary>
		public ItemUnlock Unlock { get; private set; }

		/// <summary>
		///   <para>Creates a localizable string with the specified localization <paramref name="info"/> to act as the item's name.</para>
		/// </summary>
		/// <param name="info">The localization data to initialize the localizable string with.</param>
		/// <returns>The current instance of <see cref="ItemBuilder"/>, for chaining purposes.</returns>
		/// <exception cref="ArgumentException">A localizable string that acts as the item's name already exists.</exception>
		public ItemBuilder WithName(CustomNameInfo info)
		{
			Name = RogueLibs.CreateCustomName(Info.Name, NameTypes.Item, info);
			return this;
		}
		/// <summary>
		///   <para>Creates a localizable string with the specified localization <paramref name="info"/> to act as the item's description.</para>
		/// </summary>
		/// <param name="info">The localization data to initialize the localizable string with.</param>
		/// <returns>The current instance of <see cref="ItemBuilder"/>, for chaining purposes.</returns>
		/// <exception cref="ArgumentException">A localizable string that acts as the item's description already exists.</exception>
		public ItemBuilder WithDescription(CustomNameInfo info)
		{
			Description = RogueLibs.CreateCustomName(Info.Name, NameTypes.Description, info);
			return this;
		}
		/// <summary>
		///   <para>Creates a <see cref="RogueSprite"/> with a texture from <paramref name="rawData"/> with the specified <paramref name="ppu"/> to act as the item's sprite.</para>
		/// </summary>
		/// <param name="rawData">The byte array containing a raw PNG- or JPEG-encoded image.</param>
		/// <param name="ppu">The pixels-per-unit of the custom sprite's texture.</param>
		/// <returns>The current instance of <see cref="ItemBuilder"/>, for chaining purposes.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="rawData"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="ppu"/> is less than or equal to 0.</exception>
		public ItemBuilder WithSprite(byte[] rawData, float ppu = 64f)
		{
			Sprite = RogueLibs.CreateCustomSprite(Info.Name, SpriteScope.Items, rawData, ppu);
			return this;
		}
		/// <summary>
		///   <para>Creates a <see cref="RogueSprite"/> with a texture from <paramref name="rawData"/> with the specified <paramref name="ppu"/> to act as the item's sprite.</para>
		/// </summary>
		/// <param name="rawData">The byte array containing a raw PNG- or JPEG-encoded image.</param>
		/// <param name="region">The region of the texture for the sprite to use.</param>
		/// <param name="ppu">The pixels-per-unit of the custom sprite's texture.</param>
		/// <returns>The current instance of <see cref="ItemBuilder"/>, for chaining purposes.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="rawData"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="ppu"/> is less than or equal to 0.</exception>
		public ItemBuilder WithSprite(byte[] rawData, Rect region, float ppu = 64f)
		{
			Sprite = RogueLibs.CreateCustomSprite(Info.Name, SpriteScope.Items, rawData, region, ppu);
			return this;
		}
		/// <summary>
		///   <para>Creates a default <see cref="ItemUnlock"/> for the item, that is unlocked by default.</para>
		/// </summary>
		/// <returns>The current instance of <see cref="ItemBuilder"/>, for chaining purposes.</returns>
		public ItemBuilder WithUnlock() => WithUnlock(new ItemUnlock(Info.Name, true));
		/// <summary>
		///   <para>Creates the specified <paramref name="unlock"/> for the item.</para>
		/// </summary>
		/// <param name="unlock">The unlock information to initialize.</param>
		/// <returns>The current instance of <see cref="ItemBuilder"/>, for chaining purposes.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="unlock"/> is <see langword="null"/>.</exception>
		public ItemBuilder WithUnlock(ItemUnlock unlock)
		{
			if (unlock is null) throw new ArgumentNullException(nameof(unlock));
			unlock.Name = Info.Name;
			RogueLibs.CreateCustomUnlock(unlock);
			Unlock = unlock;
			return this;
		}
	}
}
