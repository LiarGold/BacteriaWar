using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! 지역화
public class CLocalize : CComponent {

#if NGUI
	private UILabel _label = null;
#else
	private Text _text = null;
#endif

	private string _localizeKey = "";

	//! 초기화
	public override void Awake()
	{
		base.Awake();
#if NGUI
		_label = this.GetComponentInChildren<UILabel>();

#else
		_text = this.GetComponentInChildren<Text>();

#endif
		Function.LateCall((param) =>
		{
#if NGUI
			_localizeKey = _label.text;
#else
			_localizeKey = _text.text;
#endif
			this.ResetLocalize();
		}, 0.0f);
	}

	//! 상태를 갱신한다
	public override void Update()
	{
		base.Update();
	}

	//! 지역화를 리셋한다
	public void ResetLocalize()
	{
#if NGUI
		_label.text = CLocalizeManager.Instance.GetStringForKey(_localizeKey);
#else
		_text.text = CLocalizeManager.Instance.GetStringForKey(_localizeKey);
#endif
	}
}
